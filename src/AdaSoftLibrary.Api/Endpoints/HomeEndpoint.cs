using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Authentication;
using Carter;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AdaSoftLibrary.Api.Endpoints;

public class HomeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", (IHostEnvironment env, IConfiguration cfg) => @$"
            Hello {env.ApplicationName}!
            Web API: {DateTime.Now.ToLongDateString()} '{DateTime.Now.ToLongTimeString()}'
            Version: {Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version}, {File.GetLastWriteTime(AppContext.BaseDirectory)}
            ENV: {env.EnvironmentName}").WithSummary("Web API Info").WithOpenApi();

        app.MapPost("/login", Login)
            .Produces<AuthenticationResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Prihlásenie používateľa");
    }

    private async Task<IResult> Login([FromBody] AuthenticationRequest request, IUserAuthenticationService userAuthenticationService)
    {
        var result = await userAuthenticationService.AuthenticateAsync(request);

        return TypedResults.Ok(result);
    }
}
