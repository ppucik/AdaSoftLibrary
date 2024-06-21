using AdaSoftLibrary.AdaSoft.Infrastructure.Persistence;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Entities;

namespace AdaSoftLibrary.Infrastructure.Persistence.Books;

public class BookXmlRepository(AppXmlContext _xmlContext) : IBookRepository
{
    public Task<IEnumerable<Book>> GetListAsync(
        string? searchTerm = null,
        bool? borrowed = null,
        CancellationToken cancellationToken = default)
    {
        var books = borrowed switch
        {
            null => _xmlContext.Books,
            true => _xmlContext.Books.Where(x => x.Borrowed != null),
            false => _xmlContext.Books.Where(x => x.Borrowed == null)
        };

        if (!string.IsNullOrEmpty(searchTerm))
        {
            string text = searchTerm.ToUpper();

            books = books.Where(x =>
                x.Author.ToUpper().Contains(text) ||
                x.Name.ToUpper().Contains(text));
        }

        return Task.FromResult(books);
    }

    public Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = _xmlContext.Books.SingleOrDefault(x => x.Id == id);

        return Task.FromResult(book);
    }

    public Task<int> AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        book.Id = _xmlContext.Books.Max(x => x.Id) + 1;

        _xmlContext.Books.Add(book);
        _xmlContext.SaveChanges();

        return Task.FromResult(book.Id);
    }

    public Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        var b = _xmlContext.Books.Single(x => x.Id == book.Id);

        b.Author = book.Author;
        b.Name = book.Name;
        b.Year = book.Year;
        b.Description = book.Description;
        b.Borrowed = book.Borrowed;

        _xmlContext.SaveChanges();

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = _xmlContext.Books.Single(x => x.Id == id);

        _xmlContext.Books.Remove(book);
        _xmlContext.SaveChanges();

        return Task.CompletedTask;
    }
}
