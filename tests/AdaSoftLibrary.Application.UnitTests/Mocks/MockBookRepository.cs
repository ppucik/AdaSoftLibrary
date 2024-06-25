using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Entities;
using AdaSoftLibrary.Domain.Enums;
using Moq;

namespace AdaSoftLibrary.Application.UnitTests.Mocks;

public class MockBookRepository
{
    public static Mock<IBookRepository> GetBookRepository()
    {
        var books = new List<Book>()
        {
            new Book
            {
                Id = 1,
                Author = "Ernest Hemingway",
                Name = "Starec a more",
                Year = 1952,
                Borrowed = new Borrowed
                {
                    FirstName = "Peter",
                    LastName = "Prvý",
                    FromDate = new DateOnly(2016, 3, 25)
                }
            },
            new Book
            {
                Id = 2,
                Author = "William Shakespeare",
                Name = "Rómeo a Júlia",
                Year = null,
                Borrowed = new Borrowed
                {
                    FirstName = "Lukáš",
                    LastName = "Druhý",
                    FromDate = new DateOnly(2018, 6, 16)
                }
            },
            new Book
            {
                Id = 3,
                Author = "Pavol Országh Hviezdoslav",
                Name = "Krvavé sonety",
                Year = null,
                Borrowed = new Borrowed
                {
                    FirstName = "Matej",
                    LastName = "Tretí",
                    FromDate = new DateOnly(2017, 2, 1)
                }
            },
            new Book
            {
                Id = 4,
                Author = "Pavol Országh Hviezdoslav",
                Name = "Hájniková žena",
                Year = null,
                Borrowed = null
            },
            new Book
            {
                Id = 5,
                Author = "William Shakespeare",
                Name = "Hamlet",
                Year = null,
                Borrowed = new Borrowed
                {
                    FirstName = "Jozef",
                    LastName = "Štvrtý",
                    FromDate = new DateOnly(2009, 10, 25)
                }
            },
            new Book
            {
                Id = 6,
                Author = "Milo Urban",
                Name = "Živý bič",
                Year = null,
                Borrowed = null
            }
        };

        var mockBookRepository = new Mock<IBookRepository>();

        mockBookRepository
            .Setup(x => x.GetListAsync(
                It.IsAny<BookFilterEnum>(),
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((BookFilterEnum bookFilter,
                string? author, string? name, string? searchTerm,
                CancellationToken cancellationToken) =>
            {
                return bookFilter switch
                {
                    BookFilterEnum.AllBooks => books,
                    BookFilterEnum.FreeBooks => books.Where(x => !x.IsBorrowed),
                    BookFilterEnum.BorrowedBooks => books.Where(x => x.IsBorrowed),
                    _ => books
                };
            });

        mockBookRepository
            .Setup(x => x.GetByIdAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((int id, CancellationToken cancellationToken) =>
            {
                return books.FirstOrDefault(x => x.Id == id);
            });

        mockBookRepository
            .Setup(x => x.GetAuthorsAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                return books.OrderBy(x => x.Author).Select(x => x.Author).Distinct().ToList();
            });

        mockBookRepository
            .Setup(x => x.AddAsync(
                It.IsAny<Book>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book book, CancellationToken cancellationToken) =>
            {
                book.Id = books.Max(x => x.Id) + 1;
                books.Add(book);
                return book.Id;
            });

        mockBookRepository
            .Setup(x => x.UpdateAsync(
                It.IsAny<Book>(),
                It.IsAny<CancellationToken>()))
            .Returns((Book book, CancellationToken cancellationToken) =>
            {
                var b = books.Single(x => x.Id == book.Id);
                b.Author = book.Author;
                b.Name = book.Name;
                b.Year = book.Year;
                b.Description = book.Description;
                b.Borrowed = book.Borrowed;
                return Task.CompletedTask;
            });

        mockBookRepository
            .Setup(x => x.DeleteAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .Returns((int id, CancellationToken cancellationToken) =>
            {
                books.Remove(books.Single(x => x.Id == id));
                return Task.CompletedTask;
            });

        return mockBookRepository;
    }
}
