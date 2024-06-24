using AdaSoftLibrary.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Vyhľadať autora alebo názov knihy
/// </summary>
public class SearchViewModel
{
    /// <summary>
    /// Filter zoznamu kníh <see cref="BookFilterEnum" />
    /// </summary>
    [Display(Name = "Filter")]
    public BookFilterEnum BookFilter { get; set; } = BookFilterEnum.AllBooks;
    /// <summary>
    /// Vyhľadať
    /// </summary>
    [Display(Name = "Vyhľadané")]
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Autor
    /// </summary>
    [Display(Name = "Autor")]
    public string? Author { get; set; }

    /// <summary>
    /// Názov
    /// </summary>
    [Display(Name = "Názov")]
    public string? Name { get; set; }

    /// <summary>
    /// Len dostupné knihy
    /// </summary>
    [Display(Name = "Len dostupné knihy")]
    public bool OnlyAvailable { get; set; }

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(SearchTerm))
            return SearchTerm;
        else
            return $"{Author} {Name}";
    }
}
