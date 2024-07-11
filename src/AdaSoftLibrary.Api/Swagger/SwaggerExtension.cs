using Microsoft.OpenApi.Models;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

namespace AdaSoftLibrary.Api.Swagger;

public static class SwaggerExtension
{
    //[RequiresAssemblyFiles()]
    public static void AddSwagger(this IServiceCollection services, string version = "v1")
    {
        var assembly = Assembly.GetExecutingAssembly().GetName();
        var location = Assembly.GetExecutingAssembly().Location;

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.AddSwaggerBasicAuthentication();

            options.SwaggerDoc(version, new OpenApiInfo
            {
                Title = "AdaSoftLibrary Web API",
                Description = $"Version: {assembly.Version?.ToString()}, Date: {File.GetLastWriteTime(location)}",
                Version = version
            });

            //options.SchemaFilter<EnumSchemaFilter>();

            // Include XML comments
            var xmlFilePaths = Directory.GetFiles(AppContext.BaseDirectory, "AdaSoftLibrary.*.xml");

            foreach (var xmlFilePath in xmlFilePaths)
            {
                options.IncludeXmlComments(xmlFilePath, true);
                options.IncludeXmlCommentsWithRemarks(xmlFilePath, true);
                options.AddEnumsWithValuesFixFilters(o =>
                {
                    o.IncludeDescriptions = true;
                    o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;
                    o.IncludeXmlCommentsFrom(xmlFilePath);
                });
            }

            options.IncludeXmlCommentsFromInheritDocs(includeRemarks: true, excludedTypes: typeof(string));
        });
    }
}
