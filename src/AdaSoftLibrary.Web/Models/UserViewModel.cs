using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

/// <summary>
/// Prihlásenie používateľa (administrátora systému)
/// </summary>
public class UserViewModel
{
    /// <summary>
    /// Použivateľské meno
    /// </summary>
    [Display(Name = "Použivateľské meno")]
    [Required(ErrorMessage = "Použivateľské meno je povinné")]
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Heslo
    /// </summary>
    [Display(Name = "Heslo")]
    [Required(ErrorMessage = "Heslo je povinné")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Zapamätať si?
    /// </summary>
    [Display(Name = "Zapamätať si?")]
    public bool RememberMe { get; set; }
}
