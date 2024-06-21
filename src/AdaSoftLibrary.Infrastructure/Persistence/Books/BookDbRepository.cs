using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdaSoftLibrary.Infrastructure.Persistence.Books;

public class BookDbRepository(AppDbContext _dbContext) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetListAsync(
        string? searchTerm = null,
        bool? borrowed = null,
        CancellationToken cancellationToken = default)
    {
        var text = searchTerm?.ToUpper();

        return await _dbContext.Books
            .AsNoTracking()
            .Include(e => e.Borrowed)
            .Where(x => !borrowed.HasValue || x.IsBorrowed == borrowed)
            .Where(x => string.IsNullOrEmpty(text) || x.Author.ToUpper().Contains(text) || x.Name.ToUpper().Contains(text))
            .ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Books
            .AsNoTracking()
            .Include(e => e.Borrowed)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<int> AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        _dbContext.Books.Add(book);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return book.Id;
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        var b = await _dbContext.Books
            .Include(e => e.Borrowed)
            .SingleAsync(x => x.Id == book.Id, cancellationToken);

        b.Author = book.Author;
        b.Name = book.Name;
        b.Year = book.Year;
        b.Description = book.Description;
        b.Borrowed = book.Borrowed;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = await _dbContext.Books
            .SingleAsync(x => x.Id == id, cancellationToken);

        _dbContext.Books.Remove(book);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
