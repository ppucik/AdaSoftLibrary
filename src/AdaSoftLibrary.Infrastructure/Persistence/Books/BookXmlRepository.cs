using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Extensions;
using AdaSoftLibrary.Domain.Entities;
using AdaSoftLibrary.Domain.Enums;
using Fastenshtein;

namespace AdaSoftLibrary.Infrastructure.Persistence.BookList;

public class BookXmlRepository(IAppDataContext _xmlContext) : IBookRepository
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
            BookFilterEnum.AllBooks => _xmlContext.BookList,
            BookFilterEnum.FreeBooks => _xmlContext.BookList.Where(x => !x.IsBorrowed),
            BookFilterEnum.BorrowedBooks => _xmlContext.BookList.Where(x => x.IsBorrowed),
            _ => _xmlContext.BookList
        };

        if (!string.IsNullOrEmpty(author))
        {
            // Levenshtein distance algoritmus vyhľadanie autora s 90% zhodou 
            books = books.Where(x => LevenshteinMatch(x.Author, author, 85));
        }

        if (!string.IsNullOrEmpty(name))
        {
            // Levenshtein distance algoritmus vyhľadávania názvu s 75% zhodou
            books = books.Where(x => LevenshteinMatch(x.Name, name, 75));
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            // Fulltext vyhľadanie časti mena autora alebo slova v názve CI AS
            string text = searchTerm.RemoveDiacritics().ToUpper();

            books = books.Where(x =>
                x.Author.RemoveDiacritics().ToUpper().Contains(text) ||
                x.Name.RemoveDiacritics().ToUpper().Contains(text));
        }

        return Task.FromResult(books);
    }

    // Levenshtein distance algoritmus
    private static bool LevenshteinMatch(string originalStr, string searchText, int threshold)
    {
        int distance = Levenshtein.Distance(originalStr, searchText);
        double similarity = 100.0 - (distance * 100.0 / Math.Max(originalStr.Length, searchText.Length));
        return similarity >= threshold;
    }

    public Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = _xmlContext.BookList.SingleOrDefault(x => x.Id == id);

        return Task.FromResult(book);
    }

    public Task<IEnumerable<string>> GetAuthorsAsync(CancellationToken cancellationToken = default)
    {
        var authors = _xmlContext.BookList
            .OrderBy(x => x.Author)
            .Select(x => x.Author)
            .Distinct()
            .ToList() ?? Enumerable.Empty<string>();

        return Task.FromResult(authors);
    }

    public Task<int> AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        book.Id = _xmlContext.BookList.Max(x => x.Id) + 1;

        _xmlContext.BookList.Add(book);
        _xmlContext.SaveChanges();

        return Task.FromResult(book.Id);
    }

    public Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        var b = _xmlContext.BookList.Single(x => x.Id == book.Id);

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
        var book = _xmlContext.BookList.Single(x => x.Id == id);

        _xmlContext.BookList.Remove(book);
        _xmlContext.SaveChanges();

        return Task.CompletedTask;
    }
}
