using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Core.Exceptions;
using System.Net;

namespace ScheduleAnywhere.Api.Middleware;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
    {
        var (status, title) = exception switch
        {
            NotFoundException       => (HttpStatusCode.NotFound,            "Resource not found"),
            DuplicateNameException  => (HttpStatusCode.Conflict,            "Duplicate name"),
            DuplicateAbbreviationException => (HttpStatusCode.Conflict,     "Duplicate abbreviation"),
            ValidationException     => (HttpStatusCode.UnprocessableEntity, "Validation error"),
            UnauthorizedException   => (HttpStatusCode.Forbidden,           "Access denied"),
            InvalidCredentialsException => (HttpStatusCode.Unauthorized,    "Invalid credentials"),
            _                       => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
        };

        if (status == HttpStatusCode.InternalServerError)
            logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
        else
            logger.LogWarning(exception, "{Title}: {Message}", title, exception.Message);

        context.Response.StatusCode = (int)status;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = (int)status,
            Title  = title,
            Detail = exception.Message,
            Instance = context.Request.Path
        }, ct);

        return true;
    }
}
