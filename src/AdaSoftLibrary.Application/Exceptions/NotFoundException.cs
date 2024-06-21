namespace AdaSoftLibrary.Application.Exceptions;

/// <summary>
/// The 404 (Not Found) status code indicates that the origin server did
/// not find a current representation for the target resource or is not
/// willing to disclose that one exists.
/// <see href="https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4"/>
/// </summary>
public class NotFoundException : ApplicationException
{
    public NotFoundException(string name, object key)
        : base($"{name} ({key}) is not found")
    {
    }
}
