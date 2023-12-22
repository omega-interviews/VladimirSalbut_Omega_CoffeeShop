using CoffeeShop.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace CoffeeShop.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var e = exception is AggregateException aggregateException
                ? aggregateException.Flatten().InnerExceptions.FirstOrDefault()
                : exception;

            switch (e)
            {
                case UnauthorizedAccessException uae:
                    return WriteExceptionAsync(context, HttpStatusCode.Unauthorized, uae.Message);

                case ValidatingException ve:
                    return WriteExceptionAsync(context, HttpStatusCode.BadRequest,
                        ve.Errors.Any() ? ve.Errors.First().ErrorMessage.Replace("'", "") : ve.Message);

                case NotFoundException nfe:
                    return WriteExceptionAsync(context, HttpStatusCode.NotFound, nfe.Message);
            }

            return WriteExceptionAsync(context, HttpStatusCode.InternalServerError, e?.Message ?? string.Empty);
        }

        public Task WriteExceptionAsync(HttpContext context, HttpStatusCode code, object error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            return response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}
