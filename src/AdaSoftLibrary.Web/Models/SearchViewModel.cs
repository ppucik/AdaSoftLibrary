using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Vyhľadať autora alebo názov knihy
/// </summary>
public class SearchViewModel
{
    [Display(Name = "Vyhľadať")]
    public string? SearchTerm { get; set; } = null!;

    [Display(Name = "Len dostupné knihy")]
    public bool OnlyAvailable { get; set; }
}
