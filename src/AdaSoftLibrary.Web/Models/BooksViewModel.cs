using AdaSoftLibrary.Application.Books.Contracts;
using AdaSoftLibrary.Domain.Common;

namespace AdaSoftLibrary.Web.Models;

public class BooksViewModel
{
    /// <summary>
    /// Zoznam kníh
    /// </summary>
    public PagedList<GetBookResponse> Books { get; set; } = null!;

    /// <summary>
    /// Podmienky vyhľadávania <see cref="SearchViewModel" />
    /// </summary>
    public SearchViewModel Search { get; set; } = new();

    /// <summary>
    /// Je prihlásený?
    /// </summary>
    public bool IsAuthenticated { get; set; }
}
