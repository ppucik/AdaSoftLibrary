using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

public class UserViewModel
{
    [Required(ErrorMessage = "Použivateľské meno je požadované")]
    [Display(Name = "Použivateľské meno")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Heslo je požadované")]
    [DataType(DataType.Password)]
    [Display(Name = "Heslo")]
    public string Password { get; set; } = null!;

    [Display(Name = "Zapamätať si?")]
    public bool RememberMe { get; set; }
}
