using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Vymazanie knihy
/// </summary>
public class DeleteViewModel
{
    /// <summary>
    /// Autor
    /// </summary>
    [Display(Name = "Autor")]
    public string Author { get; set; } = null!;

    /// <summary>
    /// Názov
    /// </summary>
    [Display(Name = "Názov")]
    public string Name { get; set; } = null!;
}
