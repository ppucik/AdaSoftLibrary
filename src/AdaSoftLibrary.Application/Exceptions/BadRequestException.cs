namespace AdaSoftLibrary.Application.Exceptions;

/// <summary>
/// The 400 (Bad Request) status code indicates that the server cannot or 
/// will not process the request due to something that is perceived to be 
/// a client error(e.g., malformed request syntax, invalid request message 
/// framing, or deceptive request routing).
/// <see href="https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1"/>
/// </summary>
public class BadRequestException : ApplicationException
{
    public BadRequestException(string message)
        : base(message)
    {
    }
}
