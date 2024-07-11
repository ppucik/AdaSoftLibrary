namespace AdaSoftLibrary.Domain.Authentication;

/// <summary>
/// Prihlásovacie údaje používateľa
/// </summary>
public class AuthenticationRequest
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
