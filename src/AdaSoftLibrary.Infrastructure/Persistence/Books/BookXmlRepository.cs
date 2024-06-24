using AdaSoftLibrary.AdaSoft.Infrastructure.Persistence;
using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Extensions;
using AdaSoftLibrary.Domain.Entities;
using Fastenshtein;

namespace AdaSoftLibrary.Infrastructure.Persistence.Books;

public class BookXmlRepository(AppXmlContext _xmlContext) : IBookRepository
{
    public Task<IEnumerable<Book>> GetListAsync(
        BookFilterEnum bookFilter,
        string? author = null,
        string? name = null,
        string? searchTerm = null,
        CancellationToken cancellationToken = default)
    {
        var books = bookFilter switch
        {
            BookFilterEnum.AllBooks => _xmlContext.Books,
            BookFilterEnum.FreeBooks => _xmlContext.Books.Where(x => !x.IsBorrowed),
            BookFilterEnum.BorrowedBooks => _xmlContext.Books.Where(x => x.IsBorrowed),
            _ => _xmlContext.Books
        };

        if (!string.IsNullOrEmpty(author))
        {
            // Levenshtein distance algoritmus vyhľadanie autora so 90% zhodou 
            books = books.Where(x => LevenshteinMatch(x.Author, author, 85));
        }

        if (!string.IsNullOrEmpty(name))
        {
            // Levenshtein distance algoritmus vyhľadávania názvu so 75% zhodou
            books = books.Where(x => LevenshteinMatch(x.Name, name, 75));
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            // Fulltext vyhľadanie časti mena autora alebo slova názve CI AS
            string text = searchTerm.RemoveDiacritics().ToUpper();

            books = books.Where(x =>
                x.Author.RemoveDiacritics().ToUpper().Contains(text) ||
                x.Name.RemoveDiacritics().ToUpper().Contains(text));
        }

        return Task.FromResult(books);
    }

    private static bool LevenshteinMatch(string value, string searchText, int threshold)
    {
        int distance = Levenshtein.Distance(value, searchText);
        double similarity = 100.0 - (distance * 100.0 / Math.Max(value.Length, searchText.Length));
        return similarity >= threshold;
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
