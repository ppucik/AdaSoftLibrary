using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Application.Books.Queries;

/// <summary>
/// Detail knihy
/// </summary>
public class GetBookResponse
{
    public int ID { get; set; }

    /// <summary>
    /// Autor
    /// </summary>
    [Required]
    [Display(Name = "Autor")]
    public string Author { get; set; } = null!;

    /// <summary>
    /// Názov
    /// </summary>
    [Required]
    [Display(Name = "Názov")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Popis
    /// </summary>
    [Required]
    [Display(Name = "Popis")]
    public string? Description { get; set; }

    /// <summary>
    /// Rok
    /// </summary>
    [Required]
    [Display(Name = "Rok")]
    public string? Year { get; set; }

    /// <summary>
    /// Meno čitateľa
    /// </summary>
    [Display(Name = "Meno čitateľa")]
    public string? Reader { get; set; }

    /// <summary>
    /// Dátum zapožičania
    /// </summary>
    [Display(Name = "Dátum zapožičania")]
    public DateOnly? BorrowedFrom { get; set; }

    /// <summary>
    /// Dostupnosť
    /// </summary>
    [Display(Name = "Dostupnosť")]
    public bool IsBorrowed { get; set; }
}
