using AdaSoftLibrary.Application.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AdaSoftLibrary.Application.Extensions;

public static class GlobalErrorHandlerExtension
{
    public static void AddGlobalErrorHandler(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddTransient<GlobalErrorHandlerMiddleware>();
    }

    public static IApplicationBuilder UseGlobalErrorHandlerMiddleware(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalErrorHandlerMiddleware>();
}