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
    public async Task GetBooks_QueryHandler_Test()
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
    public async Task GetBooks_QueryHandler_FreeBooks_Test()
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
        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GetBooks_QueryHandler_BorrowedBooks_Test()
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
        result.Count.ShouldBe(4);
    }

    [Fact]
    public async Task GetBookById_QueryHandler_Test()
    {
        // Arrange
        var handler = new GetBook.QueryHandler(_bookRepository.Object, _mapper);

        // Act
        var query = new GetBook.Query(1);
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GetBookResponse>();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAuthors_QueryHandler_Test()
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