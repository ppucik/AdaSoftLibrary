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
    [RequiredIfCustom(nameof(IsBorrowed), true, ErrorMessage = "Meno je povinné")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Meno musí byť v rozsahu 3 až 100 znakov")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Priezvisko
    /// </summary>
    [Display(Name = "Priezvisko")]
    [RequiredIfCustom(nameof(IsBorrowed), true, ErrorMessage = "Priezvisko je povinné")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Priezvisko musí byť v rozsahu 3 až 100 znakov")]
    public string? LastName { get; set; }

    /// <summary>
    /// Dátum požičania
    /// </summary>
    [Display(Name = "Dátum požičania")]
    [RequiredIfCustom(nameof(IsBorrowed), true, ErrorMessage = "Dátum požičania je povinný")]
    [DateCurrentOrInPast()]
    [DataType(DataType.Date)]
    public DateOnly? FromDate { get; set; }

    /// <summary>
    /// Je požičaná?
    /// </summary>
    [Display(Name = "Požičaná")]
    public bool IsBorrowed { get; set; }
}
