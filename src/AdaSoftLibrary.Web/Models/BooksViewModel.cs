using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

public class BooksViewModel
{
    /// <summary>
    /// Zoznam kníh
    /// </summary>
    public IReadOnlyCollection<GetBookResponse> Books { get; set; } = null!;

    /// <summary>
    /// Počet kníh
    /// </summary>
    [Display(Name = "Počet kníh")]
    public int BooksCount => Books?.Count ?? 0;

    /// <summary>
    /// Filter zoznamu kníh <see cref="BookFilterEnum" />
    /// </summary>
    [Display(Name = "Filter")]
    public BookFilterEnum BookFilter { get; set; }

    /// <summary>
    /// Podmienky vyhľadávania
    /// </summary>
    public SearchViewModel Search { get; set; } = new();

    /// <summary>
    /// Je prihlásený?
    /// </summary>
    public bool IsAuthenticated { get; set; }
}
