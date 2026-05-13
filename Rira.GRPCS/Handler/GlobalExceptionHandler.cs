
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Rira.Core.Domain.Common.Exception;
using Rira.Utility.Framework.Common;
using System.Net;
using System.Text.Json;

public sealed class GlobalExceptionHandler
    : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var response = new ErrorResponse();

        switch (exception)
        {
            case DomainException domainException:

                _logger.LogWarning(
                    domainException,
                    domainException.Message);

                response.StatusCode = StatusCodes.Status400BadRequest;

                response.Title = "Domain validation error";

                response.Errors =
                [
                    domainException.Message
                ];

                break;

            case ValidationException validationException:

                _logger.LogWarning(
                    validationException,
                    validationException.Message);

                response.StatusCode = StatusCodes.Status400BadRequest;

                response.Title = "Validation error";

                response.Errors = validationException
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .ToList();

                break;

            case KeyNotFoundException keyNotFoundException:

                _logger.LogWarning(
                    keyNotFoundException,
                    keyNotFoundException.Message);

                response.StatusCode = StatusCodes.Status404NotFound;

                response.Title = "Resource not found";

                response.Errors =
                [
                    keyNotFoundException.Message
                ];

                break;

            default:

                _logger.LogError(
                    exception,
                    exception.Message);

                response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                response.Title = "Server error";

                response.Errors =
                [
                    "Internal server error"
                ];

                break;
        }

        httpContext.Response.StatusCode = response.StatusCode;

        httpContext.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(response);

        await httpContext.Response.WriteAsync(
            json,
            cancellationToken);

        return true;
    }
}
