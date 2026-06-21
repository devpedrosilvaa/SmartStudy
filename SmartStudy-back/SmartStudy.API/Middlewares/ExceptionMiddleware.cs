using System.Net;

namespace SmartStudy.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An unexpected error occurred");

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType= "application/json";

            var response = new ErrorResponse();

            switch(ex) 
            {
                case UnauthorizedAccessException: 
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    
                    response = new ErrorResponse
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Unauthorized access"
                    };
                    break;
                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new ErrorResponse
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Resource not found"
                    };
                    break;

                default:
                    context.Response.StatusCode =
                        (int)HttpStatusCode.InternalServerError;

                    response = new ErrorResponse
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "An internal server error occurred."
                    };
                    break;
            }

            var json = System.Text.Json.JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
