using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Books.Mapping;
using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.UnitTests.Mocks;
using AdaSoftLibrary.Domain.Constants;
using AutoMapper;
using Moq;
using Shouldly;

namespace AdaSoftLibrary.Application.UnitTests.Books.Commands;

public class CreateBookTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IBookRepository> _bookRepository;

    public CreateBookTests()
    {
        _bookRepository = MockBookRepository.GetBookRepository();

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile<BookMappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateBook_WhenCorrectValues_CreateNewBook()
    {
        // Arrange
        var handler = new CreateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new CreateBook.Command
        {
            Author = " Autor ",
            Name = " Názov ",
            Year = 2024,
            Description = "Popis"
        };

        var result = await handler.Handle(command, CancellationToken.None);
        var count = await GetBooksCount();

        // Assert
        result.ShouldBeOfType<CreateBook.Response>();

        Assert.True(result.Success);
        Assert.True(string.IsNullOrEmpty(result.Message));
        Assert.Equal(0, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(7, result.Data?.Id);
        Assert.Equal("Autor", result.Data?.Author);
        Assert.Equal("Názov", result.Data?.Name);
        Assert.Equal(2024, result.Data?.Year);
        Assert.Equal("Popis", result.Data?.Description);

        Assert.Equal(7, count);
    }

    [Fact]
    public async Task CreateBook_WithAuthorCannotBeEmpty_RetrunValidationError()
    {
        // Arrange
        var handler = new CreateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new CreateBook.Command
        {
            Author = string.Empty,
            Name = "Názov",
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<CreateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.AuthorCannotBeEmpty, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task CreateBook_WithAuthorExceeding15Char_ReturnValidationError()
    {
        // Arrange
        var handler = new CreateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new CreateBook.Command
        {
            Author = new string('a', 16),
            Name = "Názov",
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<CreateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.AuthorCannotExceed15Char, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task CreateBook_WithNameCannotBeEmpty_RetrunValidationError()
    {
        // Arrange
        var handler = new CreateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new CreateBook.Command
        {
            Author = "Autor",
            Name = string.Empty,
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<CreateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.NameCannotBeEmpty, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task CreateBook_WithNameExceeding15Char_ReturnValidationError()
    {
        // Arrange
        var handler = new CreateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new CreateBook.Command
        {
            Author = "Autor",
            Name = new string('a', 16),
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<CreateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.NameCannotExceed15Char, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task CreateBook_WithYearOutOfRange1899_ReturnValidationError()
    {
        // Arrange
        var handler = new CreateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new CreateBook.Command
        {
            Author = "Autor",
            Name = "Názov",
            Year = 1899
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<CreateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.YearOutOfRange, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task CreateBook_WithYearOutOfRange2101_ReturnValidationError()
    {
        // Arrange
        var handler = new CreateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new CreateBook.Command
        {
            Author = "Autor",
            Name = "Názov",
            Year = 2101
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<CreateBook.Response>();

        Assert.False(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Message));
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors?.Count() ?? 0);
        Assert.Equal(MessageConstants.YearOutOfRange, result.ValidationErrors?.FirstOrDefault());
        Assert.Null(result.Data);
    }

    private async Task<int> GetBooksCount()
    {
        var handler = new GetBooks.QueryHandler(_bookRepository.Object, _mapper);
        var result = await handler.Handle(new GetBooks.Query(), CancellationToken.None);

        return result?.Count ?? 0;
    }
}
