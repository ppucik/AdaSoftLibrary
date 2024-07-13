using AdaSoftLibrary.Domain.Common;
using AdaSoftLibrary.Domain.Entities;

namespace AdaSoftLibrary.Application.Common.Interfaces;

/// <summary>
/// Repozitár kníh
/// </summary>
public interface IBookRepository
{
    /// <summary>
    /// Vrátí zoznam kníh
    /// </summary>
    /// <param name="bookFilter"><see cref="BookFilter"/></param>
    /// <param name="pagination"><see cref="Pagination"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Zoznam kníh</returns>
    Task<IEnumerable<Book>> GetListAsync(
        BookFilter bookFilter,
        Pagination pagination,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Vráti celkový počet kníh
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Počet kníh</returns>
    Task<int> GetCouuntAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Vrátí informácie o kníhe
    /// </summary>
    /// <param name="id">ID knihy</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Detail knihy alebo null</returns>
    Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Vrátí zoznam autorov
    /// </summary>
    /// <param name="searchAuthor">Hľadaný autor</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<string>> GetAuthorsAsync(string? searchAuthor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Založenie novej knihy
    /// </summary>
    /// <param name="book" cref="Book"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>ID novej knihy</returns>
    Task<int> AddAsync(Book book, CancellationToken cancellationToken = default);

    /// <summary>
    /// Editácia údajov o knihe
    /// </summary>
    /// <param name="book" cref="Book"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(Book book, CancellationToken cancellationToken = default);

    /// <summary>
    /// Zmazanie knihy
    /// </summary>
    /// <param name="id">ID knihy</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
