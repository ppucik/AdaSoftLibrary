using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Common;
using AdaSoftLibrary.Domain.Entities;
using AdaSoftLibrary.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AdaSoftLibrary.Infrastructure.Persistence.Books;

public class BookDbRepository(AppDbContext _dbContext) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetListAsync(
        BookFilter bookFilter,
        Pagination pagination,
        CancellationToken cancellationToken = default)
    {
        var text = bookFilter.SearchTerm?.ToUpper();

        //TODO: Levenshtein distance Author a Name + Fulltext CI AS

        return await _dbContext.Books
            .AsNoTracking()
            .Include(e => e.Borrowed)
            .Where(x =>
                (bookFilter.BookStatus == BookStatusEnum.AllBooks) ||
                (bookFilter.BookStatus == BookStatusEnum.FreeBooks && !x.IsBorrowed) ||
                (bookFilter.BookStatus == BookStatusEnum.BorrowedBooks && x.IsBorrowed))
            .Where(x => string.IsNullOrEmpty(bookFilter.Author) || x.Author.ToUpper().Equals(bookFilter.Author.ToUpper()))
            .Where(x => string.IsNullOrEmpty(bookFilter.Name) || x.Name.ToUpper().Equals(bookFilter.Name.ToUpper()))
            .Where(x => string.IsNullOrEmpty(text) || x.Author.ToUpper().Contains(text) || x.Name.ToUpper().Contains(text))
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCouuntAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Books.CountAsync(cancellationToken);
    }

    public async Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Books
            .AsNoTracking()
            .Include(e => e.Borrowed)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<string>> GetAuthorsAsync(string? searchAuthor, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Books
            .AsNoTracking()
            .Where(x => string.IsNullOrEmpty(searchAuthor) || x.Author.Contains(searchAuthor))
            .OrderBy(x => x.Author)
            .Select(x => x.Author)
            .Distinct()
            .ToListAsync(cancellationToken);
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
