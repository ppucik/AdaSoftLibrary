using AdaSoftLibrary.Application.Books.Queries;
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
    /// <param name="bookFilter" cref="BookFilterEnum"></param>
    /// <param name="author" cref="Book.Author"></param>
    /// <param name="name" cref="Book.Name"></param>
    /// <param name="searchTerm">Časť názvu knihy alebo mena autora</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Zoznam kníh</returns>
    Task<IEnumerable<Book>> GetListAsync(
        BookFilterEnum bookFilter,
        string? author = null,
        string? name = null,
        string? searchTerm = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Vrátí informácie o kníhe
    /// </summary>
    /// <param name="id">ID knihy</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Detail knihy alebo null</returns>
    Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

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
