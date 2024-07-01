using AdaSoftLibrary.Application.Books.Contracts;
using AdaSoftLibrary.Application.Books.Mapping;
using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.UnitTests.Mocks;
using AdaSoftLibrary.Domain.Enums;
using AutoMapper;
using Moq;
using Shouldly;

namespace AdaSoftLibrary.Application.UnitTests.Books.Queries;

public class GetBookTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IBookRepository> _bookRepository;

    public GetBookTests()
    {
        _bookRepository = MockBookRepository.GetBookRepository();

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile<BookMappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetBookById_QueryHandler_ReturnBook()
    {
        // Arrange
        var handler = new GetBook.QueryHandler(_bookRepository.Object, _mapper);

        // Act
        var query = new GetBook.Query(1);
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GetBookResponse>();

        Assert.NotNull(result);
        Assert.Equal(1, result?.ID);
    }

    [Fact]
    public async Task GetBooks_QueryHandler_ReturnAllBooks()
    {
        // Arrange
        var handler = new GetBooks.QueryHandler(_bookRepository.Object, _mapper);

        // Act
        var query = new GetBooks.Query();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<List<GetBookResponse>>();
        result.Count.ShouldBe(6);
    }

    [Fact]
    public async Task GetBooks_QueryHandler_ReturnFreeBooks()
    {
        // Arrange
        var handler = new GetBooks.QueryHandler(_bookRepository.Object, _mapper);

        // Act
        var query = new GetBooks.Query()
        {
            BookFilter = BookFilterEnum.FreeBooks
        };
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<List<GetBookResponse>>();

        Assert.Equal(2, result.Count());
        Assert.All(result, book => Assert.False(book.IsBorrowed));
    }

    [Fact]
    public async Task GetBooks_QueryHandler_ReturnBorrowedBooks()
    {
        // Arrange
        var handler = new GetBooks.QueryHandler(_bookRepository.Object, _mapper);

        // Act
        var query = new GetBooks.Query()
        {
            BookFilter = BookFilterEnum.BorrowedBooks
        };
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<List<GetBookResponse>>();

        Assert.Equal(4, result.Count());
        Assert.All(result, book => Assert.True(book.IsBorrowed));
    }

    [Fact]
    public async Task GetBooks_QueryHandler_ReturnAuthor()
    {
        // Arrange
        var handler = new GetBooks.QueryHandler(_bookRepository.Object, _mapper);

        // Act
        var query = new GetBooks.Query()
        {
            Author = "william shakespeare"
        };
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<List<GetBookResponse>>();

        Assert.Equal(2, result.Count());
        Assert.All(result, book => Assert.True(book.Author == "William Shakespeare"));
    }

    [Fact]
    public async Task GetBooks_QueryHandler_ReturnName()
    {
        // Arrange
        var handler = new GetBooks.QueryHandler(_bookRepository.Object, _mapper);

        // Act
        var query = new GetBooks.Query()
        {
            Name = "hamlet"
        };
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<List<GetBookResponse>>();

        Assert.Single(result);
        Assert.All(result, book => Assert.True(book.Name == "Hamlet"));
    }

    [Fact]
    public async Task GetBooks_QueryHandler_ReturnSearchTerm()
    {
        // Arrange
        var handler = new GetBooks.QueryHandler(_bookRepository.Object, _mapper);

        // Act
        var query = new GetBooks.Query()
        {
            SearchTerm = "or"
        };
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<List<GetBookResponse>>();

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAuthors_QueryHandler_ReturnAuthors()
    {
        // Arrange
        var handler = new GetAuthors.QueryHandler(_bookRepository.Object, _mapper);

        // Act
        var query = new GetAuthors.Query();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<List<string>>();
        result.Count.ShouldBe(4);
    }
}