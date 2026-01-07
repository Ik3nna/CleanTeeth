using System.Net;
using System.Text.Json;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            } 
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred";

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case NotFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;
                case BusinessRuleException businessRuleException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    message = businessRuleException.Message;
                    break;
                case CustomValidationException customValidationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    message = string.Join("; ", customValidationException.ValidationErrors);
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            var response = new ApiResponse<object>((int)httpStatusCode, message, null);
            var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }

}