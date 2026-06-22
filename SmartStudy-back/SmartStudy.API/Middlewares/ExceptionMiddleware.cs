using System.Net;
using System.Text.Json;

namespace SmartStudy.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An unexpected error occurred");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception ex)
        {
            context.Response.ContentType = "application/json";

            ErrorResponse response;

            switch (ex)
            {
                case UnauthorizedAccessException:
                    context.Response.StatusCode =
                        (int)HttpStatusCode.Unauthorized;

                    response = new ErrorResponse
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message ?? "Unauthorized access"
                    };
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode =
                        (int)HttpStatusCode.NotFound;

                    response = new ErrorResponse
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message ?? "Resource not found"
                    };
                    break;

                case InvalidDataException:
                    context.Response.StatusCode =
                        (int)HttpStatusCode.BadRequest;

                    response = new ErrorResponse
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message ?? "Invalid data"
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

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}