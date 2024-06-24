using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Models;

public class AuthorsViewModel
{
    /// <summary>
    /// Zoznam autorov
    /// </summary>
    public IReadOnlyCollection<string> Authors { get; set; } = null!;

    /// <summary>
    /// Počet autorov
    /// </summary>
    [Display(Name = "Počet autorov")]
    public int AuthorsCount => Authors?.Count ?? 0;
}
