using AdaSoftLibrary.Domain.Authentication;

namespace AdaSoftLibrary.Application.Common.Interfaces;

public interface IUserAuthenticationService
{
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
}
