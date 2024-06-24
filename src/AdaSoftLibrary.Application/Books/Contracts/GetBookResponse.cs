using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Application.Books.Contracts;

/// <summary>
/// Detail knihy
/// </summary>
public class GetBookResponse
{
    public int ID { get; set; }

    #region Book

    /// <summary>
    /// Autor
    /// </summary>
    [Required]
    [Display(Name = "Autor")]
    public string Author { get; set; } = null!;

    /// <summary>
    /// Názov
    /// </summary>
    [Required]
    [Display(Name = "Názov")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Rok
    /// </summary>
    [Display(Name = "Rok")]
    public int? Year { get; set; }

    /// <summary>
    /// Popis
    /// </summary>
    [Display(Name = "Popis")]
    public string? Description { get; set; }

    #endregion

    #region Borrowed

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
    /// Meno čitateľa
    /// </summary>
    [Display(Name = "Meno čitateľa")]
    public string Reader => $"{LastName} {FirstName}".Trim();

    /// <summary>
    /// Dátum požičania
    /// </summary>
    [Display(Name = "Dátum požičania")]
    public DateOnly? BorrowedFrom { get; set; }

    /// <summary>
    /// Dostupnosť
    /// </summary>
    [Display(Name = "Dostupnosť")]
    public bool IsBorrowed { get; set; }

    #endregion
}
