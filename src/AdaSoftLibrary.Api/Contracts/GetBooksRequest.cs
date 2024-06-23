namespace AdaSoftLibrary.Api.Contracts;

/// <summary>
/// Dotaz na zoznam kníh
/// </summary>
public class GetBooksRequest
{
    /// <summary>
    /// Požičané knihy (True/False), všetky knihy (Null)
    /// </summary>
    public bool? Borrowed { get; set; }

    /// <summary>
    /// Časť názvu knihy alebo mena autora
    /// </summary>
    public string? SearchTerm { get; set; }
}
