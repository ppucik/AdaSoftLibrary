using Microsoft.Extensions.DependencyInjection;

namespace AdaSoftLibrary.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddAutoMapper(assembly);

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(assembly);
        });

        return services;
    }
}
