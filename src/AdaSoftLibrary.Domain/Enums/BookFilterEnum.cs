using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdaSoftLibrary.Domain.Enums;

/// <summary>
/// Filter pre zoznam kníh
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BookFilterEnum
{
    /// <summary>
    /// Všetky knihy
    /// </summary>
    [Display(Name = "Všetky knihy")]
    AllBooks = 0,

    /// <summary>
    /// Voľné knihy
    /// </summary>
    [Display(Name = "Voľné knihy")]
    FreeBooks = 1,

    /// <summary>
    /// Požičané knihy
    /// </summary>
    [Display(Name = "Požičané knihy")]
    BorrowedBooks = 2
}
