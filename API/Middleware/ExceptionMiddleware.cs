using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
   public class ExceptionMiddleware
   {
      // For checking whether we are in development mode or not
      private readonly IHostEnvironment _env;
      // For logging the message into console
      private readonly ILogger<ExceptionMiddleware> _logger;
      // For continuing to next middleware
      private readonly RequestDelegate _next;
      public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
      {
         _next = next;
         _logger = logger;
         _env = env;
      }

      public async Task InvokeAsync(HttpContext context)
      {
         try
         {
            //  Continue to next middleware if no exception
            await _next(context);
         }
         catch (Exception ex)
         {
            //  Log to console about exception
            _logger.LogError(ex, ex.Message);
            // Delcare response type to http response
            context.Response.ContentType = "application/json";
            // Delcare response status code to http response
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // In development mode we give more information
            var response = _env.IsDevelopment()
              ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
              : new ApiException((int)HttpStatusCode.InternalServerError);

            // Make property key to camelCase => Consistent about all response
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // Serialize our response class into json
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
         }
      }
   }
}