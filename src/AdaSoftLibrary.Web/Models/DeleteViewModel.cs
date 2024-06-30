using AdaSoftLibrary.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Vymazanie knihy
/// </summary>
public class DeleteViewModel
{
    /// <summary>
    /// ID
    /// </summary>
    [Required(ErrorMessage = MessageConstants.IdCannotBeEmpty)]
    public int Id { get; set; }

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
