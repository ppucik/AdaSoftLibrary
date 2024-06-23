using AdaSoftLibrary.Application.Books.Queries;

namespace AdaSoftLibrary.Api.Contracts;

/// <summary>
/// Dotaz na zoznam kníh
/// </summary>
public class GetBooksRequest
{
    /// <summary>
    /// Filter pre zoznam kníh <see cref="BookFilterEnum" />
    /// </summary>
    public BookFilterEnum BookFilter { get; set; }

    /// <summary>
    /// Časť názvu knihy alebo mena autora
    /// </summary>
    public string? SearchTerm { get; set; }
}
