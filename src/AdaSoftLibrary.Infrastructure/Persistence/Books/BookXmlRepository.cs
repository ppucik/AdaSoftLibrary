using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Extensions;
using AdaSoftLibrary.Domain.Common;
using AdaSoftLibrary.Domain.Entities;
using AdaSoftLibrary.Domain.Enums;
using Fastenshtein;

namespace AdaSoftLibrary.Infrastructure.Persistence.BookList;

public class BookXmlRepository(IAppDataContext _xmlContext) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetListAsync(
        BookFilter bookFilter,
        Pagination pagination,
        CancellationToken cancellationToken = default)
    {
        var books = bookFilter.BookStatus switch
        {
            BookStatusEnum.AllBooks => _xmlContext.BookList,
            BookStatusEnum.FreeBooks => _xmlContext.BookList.Where(x => !x.IsBorrowed),
            BookStatusEnum.BorrowedBooks => _xmlContext.BookList.Where(x => x.IsBorrowed),
            _ => _xmlContext.BookList
        };

        if (!string.IsNullOrEmpty(bookFilter.Author))
        {
            // Levenshtein distance algoritmus vyhľadanie autora s 85% zhodou 
            books = books.Where(x => LevenshteinMatch(x.Author, bookFilter.Author, 85));
        }

        if (!string.IsNullOrEmpty(bookFilter.Name))
        {
            // Levenshtein distance algoritmus vyhľadávania názvu s 75% zhodou
            books = books.Where(x => LevenshteinMatch(x.Name, bookFilter.Name, 75));
        }

        if (!string.IsNullOrEmpty(bookFilter.SearchTerm))
        {
            // Fulltext vyhľadanie časti mena autora alebo slova v názve CI AI
            string text = bookFilter.SearchTerm.RemoveDiacritics().ToUpper();

            books = books.Where(x =>
                x.Author.RemoveDiacritics().ToUpper().Contains(text) ||
                x.Name.RemoveDiacritics().ToUpper().Contains(text));
        }

        int skipNumber = (pagination.PageNumber - 1) * pagination.PageSize;

        return await Task.FromResult(books.Skip(skipNumber).Take(pagination.PageSize));
    }

    // Levenshtein distance algoritmus
    private static bool LevenshteinMatch(string originalStr, string searchText, int threshold)
    {
        int distance = Levenshtein.Distance(originalStr, searchText);
        double similarity = 100.0 - (distance * 100.0 / Math.Max(originalStr.Length, searchText.Length));

        return similarity >= threshold;
    }

    public Task<int> GetCouuntAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_xmlContext.BookList.Count());
    }

    public Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = _xmlContext.BookList.SingleOrDefault(x => x.Id == id);

        return Task.FromResult(book);
    }

    public Task<IEnumerable<string>> GetAuthorsAsync(string? searchAuthor, CancellationToken cancellationToken = default)
    {
        // Levenshtein distance algoritmus vyhľadanie autora s 50% zhodou 
        var authors = _xmlContext.BookList
            .Where(x => string.IsNullOrEmpty(searchAuthor) || LevenshteinMatch(x.Author, searchAuthor, 50))
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
