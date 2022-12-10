using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace e_Commerce.Middleware
{
    // This class is created in order to handle the errors which come from the client.
    // We will comment the code of DeveloperExceptionPage in StartUp and instead, we
    // will create this middleware so that we can handle the errors.
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareException> _logger;
        private readonly IHostEnvironment _env;

        public MiddlewareException(RequestDelegate next, ILogger<MiddlewareException> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        // The name of this method has to be like this, otherwise, it will not work
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the error using message
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                // 500 = Server Internal Error
                context.Response.StatusCode = 500;

                // This will be sent from the server to the client with Json format
                var response = new ProblemDetails
                {
                    Status = 500,
                    Detail = _env.IsDevelopment() ? ex.StackTrace?.ToString() : null,
                    Title = ex.Message
                };

                // Change all the variable names to camelCase, which is the standard format of Json
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                // Creating Json file using response with the settings of options
                var json = JsonSerializer.Serialize(response, options);

                // Sending the created response as json back to user.
                await context.Response.WriteAsync(json);
            }
        }
    }
}
