using AdaSoftLibrary.AdaSoft.Infrastructure.Persistence;
using AdaSoftLibrary.Application.Common.Configurations;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Infrastructure.Persistence;
using AdaSoftLibrary.Infrastructure.Persistence.BookList;
using AdaSoftLibrary.Infrastructure.Persistence.Books;
using AdaSoftLibrary.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdaSoftLibrary.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Persistence

        var applicationOptions = configuration.GetSection(ApplicationOptions.SECTION_NAME).Get<ApplicationOptions>();

        if (applicationOptions?.DataSourceType == DataSourceTypeEnum.SQLite)
        {
            // Add SQLite database context
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
            services.AddScoped<IAppDataContext, AppDbContext>();
            services.AddScoped<IBookRepository, BookDbRepository>();
        }
        else
        {
            // Add XML data context
            services.AddSingleton<AppXmlContext>();
            services.AddScoped<IAppDataContext, AppXmlContext>();
            services.AddScoped<IBookRepository, BookXmlRepository>();
        }

        #endregion

        #region Authentication

        services.AddSingleton<IUserAuthenticationService, UserAuthenticationService>();

        #endregion

        return services;
    }
}
