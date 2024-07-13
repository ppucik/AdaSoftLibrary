using AdaSoftLibrary.Domain.Enums;

namespace AdaSoftLibrary.Api.Contracts;

/// <summary>
/// Dotaz na zoznam kníh
/// </summary>
public class GetBooksRequest
{
    /// <summary>
    /// Filter pre zoznam kníh <see cref="BookStatusEnum" />
    /// </summary>
    public BookStatusEnum BookStatus { get; set; }

    /// <summary>
    /// Časť názvu knihy alebo mena autora
    /// </summary>
    public string? SearchTerm { get; set; }
}
