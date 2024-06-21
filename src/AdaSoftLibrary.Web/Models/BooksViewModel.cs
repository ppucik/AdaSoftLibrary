using AdaSoftLibrary.Application.Books.Queries;

namespace AdaSoftLibrary.Web.Models;

public class BooksViewModel
{
    /// <summary>
    /// Zoznam kníh
    /// </summary>
    public IReadOnlyCollection<GetBookResponse> Books { get; set; } = null!;

    /// <summary>
    /// Podmienky vyhľadávania
    /// </summary>
    public SearchViewModel? Search { get; set; }

    /// <summary>
    /// Je prihlásený ?
    /// </summary>
    public bool IsAuthenticated { get; set; }
}
