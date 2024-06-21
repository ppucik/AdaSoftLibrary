using AdaSoftLibrary.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AdaSoftLibrary.Application.Middleware;

/// <summary>
/// Global Exception Handling Middleware <see href="https://datatracker.ietf.org/doc/html/rfc7807"/>
/// </summary>
public class GlobalErrorHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

    public GlobalErrorHandlerMiddleware(ILogger<GlobalErrorHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            string problemTitle = "AdaSoft Server Error";
            string problemType = "https://tools.ietf.org/html/";
            string result = ex.Message + (ex.InnerException != null ? $"({ex.InnerException?.Message})" : string.Empty);

            int statusCode = StatusCodes.Status500InternalServerError;

            switch (ex)
            {
                case ValidationException validationException:
                    problemType += "rfc7231#section-6.5.1";
                    statusCode = StatusCodes.Status400BadRequest;
                    result = JsonSerializer.Serialize(validationException.ValdationErrors);
                    break;
                case BadRequestException badRequestException:
                    problemType += "rfc7231#section-6.5.1";
                    statusCode = StatusCodes.Status400BadRequest;
                    result = badRequestException.Message;
                    break;
                case UnauthorizedAccessException:
                    problemType += "rfc7231#section-6.5.3";
                    statusCode = StatusCodes.Status403Forbidden;
                    break;
                case NotFoundException:
                    problemType += "rfc7231#section-6.5.4";
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                case ApiException apiException:
                    statusCode = StatusCodes.Status500InternalServerError;
                    result = apiException.Message;
                    break;
            }

            _logger.LogError(ex, $"{problemTitle}: {context.Request.Path} call failed | TraceId: {context.TraceIdentifier} | Exception: {result}");

            var problemDetails = new ProblemDetails()
            {
                Title = problemTitle,
                Type = problemType,
                Instance = context.Request.Path,
                Status = statusCode,
                Detail = result,
                Extensions = { { "traceId", context.TraceIdentifier } }
            };

            string json = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(json);
        }
    }
}