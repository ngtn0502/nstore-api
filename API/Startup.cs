using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
   public class Startup
   {
      private readonly IConfiguration _configuration;
      public Startup(IConfiguration configuration)
      {
         _configuration = configuration;
      }


      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {

         // Add auto mapper as a service
         services.AddAutoMapper(typeof(MappingProfile));
         services.AddControllers();

         // Add our own application services extensions (includes Repository, GenericRepository, Services for configuring error)
         services.AddApplicationServices();
         // Add our own swagger services extention
         services.AddSwaggerDocumentation();

         services.AddDbContext<StoreContext>(x => x.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         app.UseMiddleware<ExceptionMiddleware>();

         // Add our own swagger app extension
         app.UseSwaggerDocumentation();

         // If no end point controller match it will go redirect to error page
         app.UseStatusCodePagesWithReExecute("/error/{0}");

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseStaticFiles();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
