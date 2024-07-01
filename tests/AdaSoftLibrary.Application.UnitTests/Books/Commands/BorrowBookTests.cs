using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Books.Contracts;
using AdaSoftLibrary.Application.Books.Mapping;
using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Exceptions;
using AdaSoftLibrary.Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace AdaSoftLibrary.Application.UnitTests.Books.Commands;

public class BorrowBookTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IBookRepository> _bookRepository;

    public BorrowBookTests()
    {
        _bookRepository = MockBookRepository.GetBookRepository();

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile<BookMappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task BorrowBook_WhenCorrectValues_SaveBorrowed()
    {
        // Arrange
        const int bookId = 6;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new BorrowBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new BorrowBook.Command
        {
            Id = bookId,
            FirstName = " Peter ",
            LastName = " Púčik "
        };

        var result = await handler.Handle(command, CancellationToken.None);
        var book = await GetBookFromRepository(bookId);

        // Assert
        result.ShouldBeOfType<Unit>();

        Assert.True(book?.IsBorrowed);
        Assert.Equal("Peter", book?.FirstName);
        Assert.Equal("Púčik", book?.LastName);
        Assert.Equal(today, book?.BorrowedFrom);
    }

    [Fact]
    public async Task BorrowBook_WhenNotValidID_ThrowsValidationException()
    {
        // Arrange
        const int bookId = 10;
        var handler = new ReturnBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new ReturnBook.Command(bookId);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task BorrowBook_WithEmptyFirstName_ThrowsValidationException()
    {
        // Arrange
        const int bookId = 6;
        var handler = new BorrowBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new BorrowBook.Command
        {
            Id = bookId,
            FirstName = string.Empty,
            LastName = "Púčik"
        };

        var book = await GetBookFromRepository(bookId);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

        Assert.False(book?.IsBorrowed);
        Assert.Null(book?.FirstName);
        Assert.Null(book?.LastName);
    }

    [Fact]
    public async Task BorrowBook_WithFirstNameOutOfRange_ThrowsValidationException()
    {
        // Arrange
        const int bookId = 6;
        var handler = new BorrowBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new BorrowBook.Command
        {
            Id = bookId,
            FirstName = "a",
            LastName = "Púčik"
        };

        var book = await GetBookFromRepository(bookId);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

        Assert.False(book?.IsBorrowed);
        Assert.Null(book?.FirstName);
        Assert.Null(book?.LastName);
    }

    [Fact]
    public async Task BorrowBook_WithEmptyLastName_ThrowsValidationException()
    {
        // Arrange
        const int bookId = 6;
        var handler = new BorrowBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new BorrowBook.Command
        {
            Id = bookId,
            FirstName = "Peter",
            LastName = string.Empty
        };

        var book = await GetBookFromRepository(bookId);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

        Assert.False(book?.IsBorrowed);
        Assert.Null(book?.FirstName);
        Assert.Null(book?.LastName);
    }

    [Fact]
    public async Task BorrowBook_WithLastNameOutOfRange_ThrowsValidationException()
    {
        // Arrange
        const int bookId = 6;
        var handler = new BorrowBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new BorrowBook.Command
        {
            Id = bookId,
            FirstName = "Peter",
            LastName = "a"
        };

        var book = await GetBookFromRepository(bookId);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

        Assert.False(book?.IsBorrowed);
        Assert.Null(book?.FirstName);
        Assert.Null(book?.LastName);
    }

    [Fact]
    public async Task BorrowBook_WhenBorrowed_ThrowsValidationException()
    {
        // Arrange
        const int bookId = 1;
        var handler = new BorrowBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new BorrowBook.Command
        {
            Id = bookId,
            FirstName = " Peter ",
            LastName = " Púčik "
        };

        var book = await GetBookFromRepository(bookId);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

        Assert.True(book?.IsBorrowed);
    }

    private async Task<GetBookResponse?> GetBookFromRepository(int id)
    {
        var handler = new GetBook.QueryHandler(_bookRepository.Object, _mapper);
        return await handler.Handle(new GetBook.Query(id), CancellationToken.None);
    }
}
