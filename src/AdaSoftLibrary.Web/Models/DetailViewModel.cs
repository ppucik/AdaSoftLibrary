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
    public string? FirstName { get; set; } = null!;

    /// <summary>
    /// Priezvisko
    /// </summary>
    [Display(Name = "Priezvisko")]
    public string? LastName { get; set; } = null!;

    /// <summary>
    /// Dátum požičania
    /// </summary>
    [Display(Name = "Dátum požičania")]
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
