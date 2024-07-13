using AdaSoftLibrary.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Vyhľadať autora alebo názov knihy
/// </summary>
public class SearchViewModel
{
    /// <summary>
    /// Stav kníhy <see cref="BookStatusEnum" />
    /// </summary>
    [Display(Name = "Stav kníhy")]
    public BookStatusEnum BookStatus { get; set; } = BookStatusEnum.AllBooks;

    /// <summary>
    /// Vyhľadať text
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

    /// <summary>
    /// Číslo stránky
    /// </summary>
    [FromQuery(Name = "page-number")]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Počet záznamov
    /// </summary>
    [FromQuery(Name = "page-size")]
    [Range(5, 100)]
    public int PageSize { get; set; } = 100;

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(SearchTerm))
            return SearchTerm;
        else
            return $"{Author} {Name}".Trim();
    }
}
