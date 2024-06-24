using AdaSoftLibrary.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace AdaSoftLibrary.Application.UnitTests.Common;

public class WebAppFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
    //public SqliteTestDatabase TestDatabase { get; set; } = null!;

    public IMediator CreateMediator()
    {
        var serviceScope = Services.CreateScope();

        //TestDatabase.ResetDatabase();

        return serviceScope.ServiceProvider.GetRequiredService<IMediator>();
    }

    public Task InitializeAsync() => Task.CompletedTask;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //TestDatabase = SqliteTestDatabase.CreateAndInitialize();

        builder.ConfigureTestServices(services =>
        {
            //services
            //    .RemoveAll<DbContextOptions<AppDbContext>>()
            //    .AddDbContext<AppDbContext>((sp, options) => options.UseSqlite(TestDatabase.Connection));
        });
    }

    public new Task DisposeAsync()
    {
        //TestDatabase.Dispose();

        return Task.CompletedTask;
    }
}
