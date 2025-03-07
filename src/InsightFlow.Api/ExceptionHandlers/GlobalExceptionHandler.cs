using FluentValidation;
using InsightFlow.Domain.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogCritical(
            exception,
            "Application encountered an unhandled exception of type: {ExceptionType}.",
            exception.GetType());

        var problemDetails = exception switch
        {
            BadHttpRequestException or ValidationException => new ProblemDetails
            {
                Title = DomainErrors.BadRequest.Description,
                Status = StatusCodes.Status400BadRequest,
                Detail = "https://www.rfc-editor.org/rfc/rfc9110.html#name-400-bad-request"
            },
            _ => new ProblemDetails
            {
                Title = DomainErrors.InternalServerError.Description,
                Status = StatusCodes.Status500InternalServerError,
                Detail = "https://www.rfc-editor.org/rfc/rfc9110.html#name-500-internal-server-error"
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}