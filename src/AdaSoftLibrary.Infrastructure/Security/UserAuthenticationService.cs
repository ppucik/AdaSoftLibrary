using AdaSoftLibrary.Application.Common.Configurations;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Authentication;
using Microsoft.Extensions.Options;

namespace AdaSoftLibrary.Infrastructure.Security;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly ApplicationOptions _options;

    public UserAuthenticationService(IOptions<ApplicationOptions> options)
    {
        _options = options.Value;
    }

    public Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
    {
        var response = new AuthenticationResponse
        {
            UserName = request.UserName,

            IsVerified =
                (request.UserName == _options.UserName) &&
                (request.Password == _options.Password)
        };

        return Task.FromResult(response);
    }
}
