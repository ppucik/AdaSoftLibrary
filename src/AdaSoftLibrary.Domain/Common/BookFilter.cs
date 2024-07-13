using AdaSoftLibrary.Domain.Enums;

namespace AdaSoftLibrary.Domain.Common;

/// <summary>
/// Filter pre zoznam kníh
/// </summary>
public class BookFilter
{
    /// <summary>
    /// Stav kníhy <see cref="BookStatusEnum" />
    /// </summary>
    public BookStatusEnum BookStatus { get; set; } = BookStatusEnum.AllBooks;

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
