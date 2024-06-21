using AdaSoftLibrary.Application.Books.Queries;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Detail knihy
/// </summary>
public class DetailViewModel
{
    /// <summary>
    /// Údaje o knihe
    /// </summary>
    public GetBookResponse Book { get; set; } = null!;

    /// <summary>
    /// Je prihlásený ?
    /// </summary>
    public bool IsAuthenticated { get; set; }
}
