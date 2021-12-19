using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // Because in Program.cs file, we are outside of our services container in the startup class
            // Add scoped 
            // using statement is a way to handle lifettime problem in .NET 
            using (var scope = host.Services.CreateScope())
            {
                // Create service
                var services = scope.ServiceProvider;
                // Create logger to log exception to terminal
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    
                try
                {
                    // Get our context
                    var context = services.GetRequiredService<StoreContext>();
                    // MigrateAsync is  method that apply our pendding migration context and create database if it now exist
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
