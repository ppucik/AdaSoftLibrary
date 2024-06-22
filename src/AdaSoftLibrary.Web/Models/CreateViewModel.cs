using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Zaevidovanie knihy
/// </summary>
public class CreateViewModel
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
    [Range(0, 9999, ErrorMessage = "Rok musí byť v rozsahu 0 až 9999")]
    public int? Year { get; set; }

    /// <summary>
    /// Popis
    /// </summary>
    [Display(Name = "Popis")]
    public string? Description { get; set; }
}
