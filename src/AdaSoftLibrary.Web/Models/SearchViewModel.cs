using AdaSoftLibrary.Web.Common;
using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Vyhľadať autora alebo názov knihy
/// </summary>
public class SearchViewModel
{
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
    /// Vyhľadať knihu
    /// </summary>
    [Display(Name = "Vyhľadať")]
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Zoznam kníh
    /// </summary>
    [Display(Name = "")]
    BookFilterEnum BookFilter { get; set; }

    /// <summary>
    /// Len dostupné knihy
    /// </summary>
    [Display(Name = "Len dostupné knihy")]
    public bool OnlyAvailable { get; set; }
}
