namespace AdaSoftLibrary.Api.Contractsô;

/// <summary>
/// Vypožičanie knihy
/// </summary>
public class BorrowBookRequest
{
    public int BookId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
