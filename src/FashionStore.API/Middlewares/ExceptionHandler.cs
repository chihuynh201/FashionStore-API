using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace FashionStore.API.Middleware;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, $"An unhandled exception occurred: {exception.Message}");

        httpContext.Response.StatusCode = StatusCodes.Status200OK;
        httpContext.Response.ContentType = "application/json";

        var response = ActionResponse.CreateResponse(ResponseCode.Error);
        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            response = ActionResponse.CreateResponse(ResponseCode.InvalidData, errors);

        }

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
