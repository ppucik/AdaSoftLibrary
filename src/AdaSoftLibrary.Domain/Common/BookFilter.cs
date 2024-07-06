using AdaSoftLibrary.Domain.Enums;

namespace AdaSoftLibrary.Domain.Common;

public class BookFilter
{
    /// <summary>
    /// Filter zoznamu kníh <see cref="BookFilterEnum" />
    /// </summary>
    public BookFilterEnum Availability { get; set; } = BookFilterEnum.AllBooks;

    /// <summary>
    /// Hľadaný text (autor, názov, popis)
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Autor
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Názov
    /// </summary>
    public string? Name { get; set; }
}
