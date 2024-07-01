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

public class ReturnBookTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IBookRepository> _bookRepository;

    public ReturnBookTests()
    {
        _bookRepository = MockBookRepository.GetBookRepository();

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile<BookMappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task ReturnBook_WhenValidID_SaveBorrowNull()
    {
        // Arrange
        const int bookId = 1;
        var handler = new ReturnBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new ReturnBook.Command(bookId);
        var result = await handler.Handle(command, CancellationToken.None);
        var book = await GetBookFromRepository(bookId);

        // Assert
        result.ShouldBeOfType<Unit>();

        Assert.False(book?.IsBorrowed);
    }

    [Fact]
    public async Task ReturnBook_WhenNotValidID_ThrowNotFoundException()
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
    public async Task ReturnBook_WhenNotBorrowed_ThrowValidationException()
    {
        // Arrange
        const int bookId = 6;
        var handler = new ReturnBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new ReturnBook.Command(bookId);
        var book = await GetBookFromRepository(bookId);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

        Assert.False(book?.IsBorrowed);
    }

    private async Task<GetBookResponse?> GetBookFromRepository(int id)
    {
        var handler = new GetBook.QueryHandler(_bookRepository.Object, _mapper);
        return await handler.Handle(new GetBook.Query(id), CancellationToken.None);
    }
}
