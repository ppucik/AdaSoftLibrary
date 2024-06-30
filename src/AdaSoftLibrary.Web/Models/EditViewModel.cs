using AdaSoftLibrary.Domain.Constants;
using AdaSoftLibrary.Web.Common;
using ExpressiveAnnotations.Attributes;
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
    [Required(ErrorMessage = MessageConstants.IdCannotBeEmpty)]
    public int Id { get; set; }

    /// <summary>
    /// Meno
    /// </summary>
    [Display(Name = "Meno")]
    [RequiredIf(nameof(IsBorrowed), ErrorMessage = MessageConstants.FirstNameCannotBeEmpty)]
    [StringLength(100, MinimumLength = 3, ErrorMessage = MessageConstants.FirstNameOutOfRange)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Priezvisko
    /// </summary>
    [Display(Name = "Priezvisko")]
    [RequiredIf(nameof(IsBorrowed), ErrorMessage = MessageConstants.LastNameCannotBeEmpty)]
    [StringLength(100, MinimumLength = 3, ErrorMessage = MessageConstants.LastNameOutOfRange)]
    public string? LastName { get; set; }

    /// <summary>
    /// Dátum požičania
    /// </summary>
    [Display(Name = "Dátum požičania")]
    [RequiredIf(nameof(IsBorrowed), ErrorMessage = MessageConstants.FromDateCannotBeEmpty)]
    [DateCurrentOrInPast()]
    [DataType(DataType.Date)]
    public DateOnly? FromDate { get; set; }

    /// <summary>
    /// Je požičaná?
    /// </summary>
    [Display(Name = "Požičaná")]
    public bool IsBorrowed { get; set; }
}
