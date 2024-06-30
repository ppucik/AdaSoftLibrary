using AdaSoftLibrary.Domain.Constants;
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
    [Required(ErrorMessage = MessageConstants.AuthorCannotBeEmpty)]
    [StringLength(15, ErrorMessage = MessageConstants.AuthorCannotExceed15Char)]
    public string Author { get; set; } = null!;

    /// <summary>
    /// Názov
    /// </summary>
    [Display(Name = "Názov")]
    [Required(ErrorMessage = MessageConstants.NameCannotBeEmpty)]
    [StringLength(15, ErrorMessage = MessageConstants.NameCannotExceed15Char)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Rok
    /// </summary>
    [Display(Name = "Rok")]
    [Range(1900, 2100, ErrorMessage = MessageConstants.YearOutOfRange)]
    public int? Year { get; set; }

    /// <summary>
    /// Popis
    /// </summary>
    [Display(Name = "Popis")]
    public string? Description { get; set; }
}
