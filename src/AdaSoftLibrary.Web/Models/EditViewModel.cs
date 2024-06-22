using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Editácia knihy
/// </summary>
public class EditViewModel
{
    /// <summary>
    /// Autor
    /// </summary>
    [Required(ErrorMessage = "Autor je požadovaný")]
    [Display(Name = "Autor")]
    public string Author { get; set; } = null!;

    /// <summary>
    /// Názov
    /// </summary>
    [Required(ErrorMessage = "Názov je požadovaný")]
    [Display(Name = "Názov")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Rok
    /// </summary>
    [Display(Name = "Rok")]
    public int? Year { get; set; }

    /// <summary>
    /// Popis
    /// </summary>
    [Required]
    [Display(Name = "Popis")]
    public string? Description { get; set; }

    /// <summary>
    /// Je zapožičaná
    /// </summary>
    [Display(Name = "Zapožičaná")]
    public bool IsBorrowed { get; set; }
}
