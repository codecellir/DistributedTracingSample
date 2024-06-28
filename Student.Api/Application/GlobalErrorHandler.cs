using Microsoft.AspNetCore.Diagnostics;
using Students.BuildingBlock.Exceptions;
using System.Net;
using FluentValidation;
using Students.BuildingBlock;

namespace Students.Api.Application;

public class GlobalErrorHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        Type type = exception.GetType();

        return type switch
        {
            var ex when ex == typeof(AppException) => await HandleException(httpContext, exception,
                HttpStatusCode.BadRequest),

            var ex when ex == typeof(ValidationException) => await HandleValidationException(httpContext, exception,
                HttpStatusCode.BadRequest),

            var ex when ex == typeof(NotFoundException) => await HandleException(httpContext, exception,
                HttpStatusCode.NotFound),

            _ => await HandleException(httpContext, exception, HttpStatusCode.BadRequest),
        };
    }

    async Task<bool> HandleException(HttpContext httpContext, Exception exception, HttpStatusCode code)
    {
        httpContext.Response.StatusCode = (int)code;
        var response = BaseResult.Fail(exception.Message);
        await httpContext.Response.WriteAsJsonAsync(response);
        return true;
    }
    async Task<bool> HandleValidationException(HttpContext httpContext, Exception exception, HttpStatusCode code)
    {
        var validationException = (ValidationException)exception;
        var messages = string.Join(',', validationException.Errors.Select(d => d.ErrorMessage));
        httpContext.Response.StatusCode = (int)code;
        var response = BaseResult.Fail(messages);
        await httpContext.Response.WriteAsJsonAsync(response);
        return true;
    }
}