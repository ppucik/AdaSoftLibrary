using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Books.Contracts;
using AdaSoftLibrary.Application.Books.Mapping;
using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Exceptions;
using AdaSoftLibrary.Application.UnitTests.Mocks;
using AdaSoftLibrary.Domain.Constants;
using AutoMapper;
using Moq;
using Shouldly;

namespace AdaSoftLibrary.Application.UnitTests.Books.Commands;

public class UpdateBookTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IBookRepository> _bookRepository;

    public UpdateBookTests()
    {
        _bookRepository = MockBookRepository.GetBookRepository();

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile<BookMappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task UpdateBook_WhenCorrectValues_UpdateBook()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = " Autor ",
            Name = " Názov ",
            Year = 2024,
            Description = "Popis",
            FirstName = " Peter ",
            LastName = " Púčik ",
            BorrowedFrom = today
        };

        var result = await handler.Handle(command, CancellationToken.None);
        var book = await GetBookFromRepository(bookId);

        // Assert
        result.ShouldBeOfType<UpdateBook.Response>();

        Assert.True(result.Success);
        Assert.True(string.IsNullOrEmpty(result.Message));
        Assert.Equal(0, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(bookId, result.Data?.Id);
        Assert.Equal("Autor", result.Data?.Author);
        Assert.Equal("Názov", result.Data?.Name);
        Assert.Equal(2024, result.Data?.Year);
        Assert.Equal("Popis", result.Data?.Description);
        Assert.Equal("Peter", result.Data?.Borrowed?.FirstName);
        Assert.Equal("Púčik", result.Data?.Borrowed?.LastName);
        Assert.Equal(today, result.Data?.Borrowed?.FromDate);
    }

    [Fact]
    public async Task UpdateBook_WhenNotValidID_ThrowNotFoundException()
    {
        // Arrange
        const int bookId = 10;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = " Autor ",
            Name = " Názov ",
            Year = 2024,
            Description = "Popis",
            FirstName = " Peter ",
            LastName = " Púčik ",
            BorrowedFrom = today
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateBook_WithAuthorCannotBeEmpty_RetrunValidationError()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = string.Empty,
            Name = "Názov",
            FirstName = "Peter",
            LastName = "Púčik",
            BorrowedFrom = today
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<UpdateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.AuthorCannotBeEmpty, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithAuthorExceeding15Char_ReturnValidationError()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = new string('a', 16),
            Name = "Názov",
            FirstName = "Peter",
            LastName = "Púčik",
            BorrowedFrom = today
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<UpdateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.AuthorCannotExceed15Char, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithNameCannotBeEmpty_RetrunValidationError()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = "Autor",
            Name = string.Empty,
            FirstName = "Peter",
            LastName = "Púčik",
            BorrowedFrom = today
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<UpdateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.NameCannotBeEmpty, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithNameExceeding15Char_ReturnValidationError()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = "Autor",
            Name = new string('a', 16),
            FirstName = "Peter",
            LastName = "Púčik",
            BorrowedFrom = today
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<UpdateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.NameCannotExceed15Char, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithYearOutOfRange1899_ReturnValidationError()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = "Autor",
            Name = "Názov",
            Year = 1899,
            FirstName = "Peter",
            LastName = "Púčik",
            BorrowedFrom = today
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<UpdateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.YearOutOfRange, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithYearOutOfRange2101_ReturnValidationError()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = "Autor",
            Name = "Názov",
            Year = 2101,
            FirstName = "Peter",
            LastName = "Púčik",
            BorrowedFrom = today
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<UpdateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.YearOutOfRange, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithFirstNameCannotBeEmpty_ReturnValidationError()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = "Autor",
            Name = "Názov",
            FirstName = string.Empty,
            LastName = "Púčik",
            BorrowedFrom = today
        };

        // Assert
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.FirstNameCannotBeEmpty, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithFirstNameOutOfRange_ReturnValidationError()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = "Autor",
            Name = "Názov",
            FirstName = "a",
            LastName = "Púčik",
            BorrowedFrom = today
        };

        // Assert
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.FirstNameOutOfRange, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithLastNameCannotBeEmpty_ReturnValidationError()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = "Autor",
            Name = "Názov",
            FirstName = "Peter",
            LastName = string.Empty,
            BorrowedFrom = today
        };

        // Assert
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.LastNameCannotBeEmpty, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithLastNameOutOfRange_ReturnValidationError()
    {
        // Arrange
        const int bookId = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = "Autor",
            Name = "Názov",
            FirstName = "Peter",
            LastName = "a",
            BorrowedFrom = today
        };

        // Assert
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.LastNameOutOfRange, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithFromDateCannotBeEmpty_ReturnValidationError()
    {
        // Arrange
        const int bookId = 1;
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = "Autor",
            Name = "Názov",
            FirstName = "Peter",
            LastName = "Púčik",
            BorrowedFrom = null
        };

        // Assert
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.FromDateCannotBeEmpty, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateBook_WithFromDateCurrentOrInPast_ReturnValidationError()
    {
        // Arrange
        const int bookId = 1;
        var tomorrow = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = bookId,
            Author = "Autor",
            Name = "Názov",
            FirstName = "Peter",
            LastName = "Púčik",
            BorrowedFrom = tomorrow
        };

        // Assert
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.FromDateCurrentOrInPast, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    private async Task<GetBookResponse?> GetBookFromRepository(int id)
    {
        var handler = new GetBook.QueryHandler(_bookRepository.Object, _mapper);
        return await handler.Handle(new GetBook.Query(id), CancellationToken.None);
    }
}
