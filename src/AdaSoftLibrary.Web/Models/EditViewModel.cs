using AdaSoftLibrary.Web.Common;
using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Editácia knihy
/// </summary>
public class EditViewModel : BookViewModel
{
    /// <summary>
    /// ID
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Meno
    /// </summary>
    [Display(Name = "Meno")]
    [Required(ErrorMessage = "Meno je povinné")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Meno musí byť v rozsahu 3 až 100 znakov")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Priezvisko
    /// </summary>
    [Display(Name = "Priezvisko")]
    [Required(ErrorMessage = "Priezvisko je povinné")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Priezvisko musí byť v rozsahu 3 až 100 znakov")]
    public string? LastName { get; set; }

    /// <summary>
    /// Dátum zapožičania
    /// </summary>
    [Display(Name = "Dátum zapožičania")]
    [Required(ErrorMessage = "Dátum zapožičania je povinný")]
    [DateCurrentOrInPast()]
    [DataType(DataType.Date)]
    public DateOnly? FromDate { get; set; }

    /// <summary>
    /// Je zapožičaná
    /// </summary>
    [Display(Name = "Zapožičaná")]
    public bool IsBorrowed { get; set; }
}
