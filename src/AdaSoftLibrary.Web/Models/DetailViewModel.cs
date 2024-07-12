using AdaSoftLibrary.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Detail knihy
/// </summary>
public class DetailViewModel : BookViewModel
{
    public int Id { get; set; }

    /// <summary>
    /// Meno čitateľa
    /// </summary>
    [Display(Name = "Meno čitateľa")]
    public string Reader => $"{LastName} {FirstName}".Trim();

    /// <summary>
    /// Meno
    /// </summary>
    [Display(Name = "Meno")]
    [Required(ErrorMessage = MessageConstants.FirstNameCannotBeEmpty)]
    [StringLength(100, MinimumLength = 3, ErrorMessage = MessageConstants.FirstNameOutOfRange)]
    public string? FirstName { get; set; } = null!;

    /// <summary>
    /// Priezvisko
    /// </summary>
    [Display(Name = "Priezvisko")]
    [Required(ErrorMessage = MessageConstants.LastNameCannotBeEmpty)]
    [StringLength(100, MinimumLength = 3, ErrorMessage = MessageConstants.LastNameOutOfRange)]
    public string? LastName { get; set; } = null!;

    /// <summary>
    /// Dátum požičania
    /// </summary>
    [Display(Name = "Dátum požičania")]
    [DataType(DataType.Date)]
    public DateOnly? FromDate { get; set; }

    /// <summary>
    /// Dostupnosť
    /// </summary>
    [Display(Name = "Dostupnosť")]
    public bool IsBorrowed { get; init; }

    /// <summary>
    /// Je prihlásený ?
    /// </summary>
    public bool IsAuthenticated { get; init; }
}
