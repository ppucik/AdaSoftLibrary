using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

namespace AdaSoftLibrary.Application.Extensions;

public static class SwaggerExtension
{
    public static void AddSwagger(this IServiceCollection services, string version = "v1")
    {
        var assembly = Assembly.GetExecutingAssembly().GetName();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(version, new OpenApiInfo
            {
                Title = "AdaSoftLibrary Web API",
                Description = $"Version: {assembly.Version?.ToString()}, Date: {File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location)}",
                Version = version
            });

            // Include XML comments
            var xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{assembly.Name}.xml");
            options.IncludeXmlComments(xmlFilePath, true);
            options.IncludeXmlCommentsWithRemarks(xmlFilePath, true);
            options.IncludeXmlCommentsFromInheritDocs(includeRemarks: true, excludedTypes: typeof(string));

            options.AddEnumsWithValuesFixFilters(o =>
            {
                o.IncludeDescriptions = true;
                o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;
                o.IncludeXmlCommentsFrom(Path.Combine(AppContext.BaseDirectory, "AdaSoftLibrary.Domain.xml"));
            });
        });
    }
}
