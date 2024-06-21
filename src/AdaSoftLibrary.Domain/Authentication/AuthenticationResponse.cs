namespace AdaSoftLibrary.Domain.Authentication;

public class AuthenticationResponse
{
    public string UserName { get; set; } = null!;

    public bool IsVerified { get; set; }
}
