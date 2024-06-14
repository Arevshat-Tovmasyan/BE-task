using BE_task.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace BE_task.Middlewares
{
    public class ExceptionMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        private readonly RequestDelegate request;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            request = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext context) => this.InvokeAsync(context);

        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.request(context);
            }
            catch (MyException ex)
            {
                var response = new ErrorResponse(ex.Message);

                // Set http status code and content type
                context.Response.ContentType = JsonContentType;
                context.Response.StatusCode = (int)ex.ErrorCode;

                // Writes / Returns error model to the response
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, SerializerSettings));

                // Log error 
                _logger.LogError(ex.ToString());
            }
            catch (Exception ex)
            {
                var response = new ErrorResponse("Unhandled Server Error");

                // Set http status code and content type
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = JsonContentType;

                // Writes / Returns error model to the response
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, SerializerSettings));

                // Log error 
                _logger.LogError(ex.ToString());
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }

    public class ErrorResponse
    {
        public string Message { get; private set; }

        public ErrorResponse(string message)
        {
            Message = message;
        }
    }
}
