using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AdaSoftLibrary.Api.Swagger;

public static class SwaggerAuthorization
{
    private const string BasicAuthSchemeName = "AdaSoftLibrary BasicAuth";

    public static void AddSwaggerBasicAuthentication(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.AddSecurityDefinition(BasicAuthSchemeName, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "Basic",
            Description = "Basic HTTP authentication",
        });

        var scheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = BasicAuthSchemeName,
            },
            In = ParameterLocation.Header,
        };

        var requirement = new OpenApiSecurityRequirement
        {
            {
                scheme,
                Array.Empty<string>()
            },
        };

        swaggerGenOptions.AddSecurityRequirement(requirement);
    }
}
