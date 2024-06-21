using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AdaSoftLibrary.Application.Common.Configurations;

public class ApplicationOptionsSetup : IConfigureOptions<ApplicationOptions>
{
    private const string SECTION_NAME = "Application";
    private readonly IConfiguration _configuration;

    public ApplicationOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(ApplicationOptions options)
    {
        _configuration
            .GetSection(SECTION_NAME)
            .Bind(options);
    }
}
