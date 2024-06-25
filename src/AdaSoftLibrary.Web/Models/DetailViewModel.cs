using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Detail knihy
/// </summary>
public class DetailViewModel : BookViewModel
{
    public int ID { get; set; }

    /// <summary>
    /// Meno čitateľa
    /// </summary>
    [Display(Name = "Meno čitateľa")]
    public string Reader => $"{LastName} {FirstName}".Trim();

    /// <summary>
    /// Meno
    /// </summary>
    [Display(Name = "Meno")]
    [Required(ErrorMessage = "Meno je povinné")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Meno musí byť v rozsahu 3 až 100 znakov")]
    public string? FirstName { get; set; } = null!;

    /// <summary>
    /// Priezvisko
    /// </summary>
    [Display(Name = "Priezvisko")]
    [Required(ErrorMessage = "Priezvisko je povinné")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Priezvisko musí byť v rozsahu 3 až 100 znakov")]
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
    public bool IsBorrowed { get; init; }

    /// <summary>
    /// Je prihlásený ?
    /// </summary>
    public bool IsAuthenticated { get; init; }
}
