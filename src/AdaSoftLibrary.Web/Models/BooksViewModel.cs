using AdaSoftLibrary.Application.Books.Contracts;
using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

public class BooksViewModel
{
    /// <summary>
    /// Zoznam kníh
    /// </summary>
    public IReadOnlyCollection<GetBookResponse> Books { get; set; } = null!;

    /// <summary>
    /// Podmienky vyhľadávania <see cref="SearchViewModel" />
    /// </summary>
    public SearchViewModel Search { get; set; } = new();

    /// <summary>
    /// Počet kníh
    /// </summary>
    [Display(Name = "Počet kníh")]
    public int BooksCount => Books?.Count ?? 0;

    /// <summary>
    /// Je prihlásený?
    /// </summary>
    public bool IsAuthenticated { get; set; }
}
