using AdaSoftLibrary.Application.Common.Interfaces;
using System.Text;

namespace AdaSoftLibrary.Api.Filters;

public class BasicAuthenticationFilter : IEndpointFilter
{
    private readonly IUserAuthenticationService _userAuthenticationService;
    private const string BasicHeaderName = "Authorization";
    private const string BasicScheme = "Basic";

    public BasicAuthenticationFilter(IUserAuthenticationService userAuthenticationService)
    {
        _userAuthenticationService = userAuthenticationService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(BasicHeaderName, out var authorizationHeader))
        {
            return TypedResults.Problem("Basic header is missing", default, StatusCodes.Status401Unauthorized);
        }

        var headerValue = authorizationHeader.ToString();

        if (!headerValue.StartsWith($"{BasicScheme} ", StringComparison.OrdinalIgnoreCase))
        {
            return TypedResults.Problem("Basic scheme is missing", default, StatusCodes.Status401Unauthorized);
        }

        if (!TryGetUserCredentials(headerValue, out var userCredentials))
        {
            return TypedResults.Problem("Authorization header is invalid", default, StatusCodes.Status401Unauthorized);
        }

        var authenticationRequest = new Domain.Authentication.AuthenticationRequest
        {
            UserName = userCredentials.username,
            Password = userCredentials.password,
        };

        var authenticationResponse = await _userAuthenticationService.AuthenticateAsync(authenticationRequest);

        if (!authenticationResponse.IsVerified)
        {
            return TypedResults.Problem("User credentials are invalid", default, StatusCodes.Status401Unauthorized);
        }

        return await next(context);
    }

    private static bool TryGetUserCredentials(string headerValue, out (string username, string password) userCredentials)
    {
        try
        {
            var encodedCredentials = headerValue.Split(' ', 2)[1];
            var credentialsBytes = Convert.FromBase64String(encodedCredentials);
            var credentialsString = Encoding.UTF8.GetString(credentialsBytes);
            var credentials = credentialsString.Split(':', 2);
            userCredentials = (credentials[0], credentials[1]);
            return true;
        }
        catch
        {
            userCredentials = default;
            return false;
        }
    }
}
