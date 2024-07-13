using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Domain.Common;

/// <summary>
/// Stránkovanie a radenie záznamov
/// </summary>
public class Pagination
{
    /// <summary>
    /// Číslo stránky
    /// </summary>
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Počet záznamov
    /// </summary>
    [Range(5, 100)]
    public int PageSize { get; set; } = 100;

    /// <summary>
    /// Radenie záznamov
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Smer radenia - true DESC; otherwise ASC
    /// </summary>
    public bool? SortDirectionDesc { get; set; }
}
