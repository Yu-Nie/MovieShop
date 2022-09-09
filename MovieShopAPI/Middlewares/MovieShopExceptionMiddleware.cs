using ApplicationCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieShopAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MovieShopExceptionMiddleware> _logger;

        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                // read info from the httpcontext object and log it 
                _logger.LogInformation("Inside the Middleware");
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var exceptionDetails = new ErrorModel
                {
                    Message = ex.Message,
                    // StackTrace = ex.StackTrace,
                    ExceptionDateTime = DateTime.UtcNow,
                    // ExceptionType = ex.GetType().ToString(),
                    Path = httpContext.Request.Path,
                    HttpRequestType = httpContext.Request.Method,
                    User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null
                };

                // first catch the exception
                // check the exception type, message
                // check stacktrace, where the exception happend
                // when the exception happend
                // for which URL and which Http method (controller, action method)
                // for which user, if user is logged in

                // save all this information somewhere, text files, json files or database
                // system.IO to create text files
                // asp.net core has builting logging mechanism, (ILogger) which can be used by any 3rd party log provide
                // *SeriLog* and NLog
                _logger.LogError("Exception happened, log this to text or Json files using SeriLog");

                // return Http status code
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize(new { exceptionDetails });
                await httpContext.Response.WriteAsync(result);

                // httpContext.Response.Redirect("/home/error")
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MovieShopExceptionMiddlewareExtensions
    {
        // extension method, that extends IApplicationBuilder
        public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}
