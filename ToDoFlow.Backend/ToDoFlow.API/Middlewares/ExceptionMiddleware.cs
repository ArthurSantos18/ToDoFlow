using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using ToDoFlow.Application.Services.Utils;

namespace ToDoFlow.API.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                await HandleExceptionAsync(context, ex);
            }
        }
        
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            
            int httpStatus;
            string message;

            switch (exception)
            {
                case ArgumentException:
                    httpStatus = StatusCodes.Status400BadRequest;
                    break;

                case ValidationException:
                    httpStatus = StatusCodes.Status400BadRequest;
                    break;

                case UnauthorizedAccessException:
                    httpStatus = StatusCodes.Status401Unauthorized;
                    break;

                case KeyNotFoundException:
                    httpStatus = StatusCodes.Status404NotFound;
                    break;

                default:
                    httpStatus = StatusCodes.Status500InternalServerError;
                    break;
            }
            
            message = exception.Message;


            var response = new ApiResponse<object>(
                data: null,
                success: false,
                message: message,
                httpStatus: httpStatus
            );

            context.Response.StatusCode = httpStatus;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
