namespace AdaSoftLibrary.Api.Contracts;

/// <summary>
/// Editácia údajov o knihe
/// </summary>
public class UpdateBookRequest
{
    public string Author { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int? Year { get; set; }
    public string? Description { get; set; }
}
