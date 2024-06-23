using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Údaje o knihe
/// </summary>
public class BookViewModel
{
    /// <summary>
    /// Autor
    /// </summary>
    [Display(Name = "Autor")]
    [Required(ErrorMessage = "Autor je povinný")]
    //[StringLength(250, ErrorMessage = "Autor nesmie presiahnuť 250 znakov")]
    [StringLength(15, ErrorMessage = "Autor nesmie presiahnuť 15 znakov")]
    public string Author { get; set; } = null!;

    /// <summary>
    /// Názov
    /// </summary>
    [Display(Name = "Názov")]
    [Required(ErrorMessage = "Názov je povinný")]
    //[StringLength(1000, ErrorMessage = "Názov nesmie presiahnuť 1000 znakov")]
    [StringLength(15, ErrorMessage = "Názov nesmie presiahnuť 15 znakov")]
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
