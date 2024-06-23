using AdaSoftLibrary.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AdaSoftLibrary.Web.Filters;

public class GlobalExceptionFilters : IExceptionFilter
{
    private readonly ILogger _logger;

    public GlobalExceptionFilters(ILogger<GlobalExceptionFilters> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (!context.ExceptionHandled)
        {
            var exception = context.Exception;

            switch (true)
            {
                case bool _ when exception is BadRequestException:
                    context.Result = new BadRequestResult();
                    break;

                case bool _ when exception is NotFoundException:
                    context.Result = new NotFoundResult();
                    break;

                case bool _ when exception is InvalidOperationException:
                    context.Result = new BadRequestResult();
                    break;

                case bool _ when exception is UnauthorizedAccessException:
                    context.Result = GetCustomException(exception, HttpStatusCode.Unauthorized);
                    break;

                default:
                    context.Result = GetCustomException(exception, HttpStatusCode.InternalServerError);
                    break;
            }

            _logger.LogError($"GlobalExceptionFilter: Error in {context.ActionDescriptor.DisplayName}. {exception.Message}. Stack Trace: {exception.StackTrace}");
        }
    }

    // Custom Exception message to be returned to the UI
    private IActionResult GetCustomException(Exception exception, HttpStatusCode statusCode)
    {
        return new ObjectResult(exception.Message) { StatusCode = (int)statusCode };
    }
}

