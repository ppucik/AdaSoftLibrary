namespace AdaSoftLibrary.Api.Contracts;

/// <summary>
/// Zaevidovanie novej knihy
/// </summary>
public class CreateBookRequest
{
    public string Author { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int? Year { get; set; }
    public string? Description { get; set; }
}
