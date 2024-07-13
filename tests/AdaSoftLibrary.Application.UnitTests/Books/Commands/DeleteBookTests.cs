using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Books.Mapping;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.Exceptions;
using AdaSoftLibrary.Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace AdaSoftLibrary.Application.UnitTests.Books.Commands;

public class DeleteBookTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IBookRepository> _bookRepository;

    public DeleteBookTests()
    {
        _bookRepository = MockBookRepository.GetBookRepository();

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile<BookMappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task DeleteBook_WhenValidID_RemoveBook()
    {
        // Arrange
        const int bookId = 1;
        var handler = new DeleteBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new DeleteBook.Command(bookId);
        var result = await handler.Handle(command, CancellationToken.None);
        var count = await _bookRepository.Object.GetCouuntAsync(CancellationToken.None);

        // Assert
        result.ShouldBeOfType<Unit>();

        Assert.Equal(5, count);
    }

    [Fact]
    public async Task DeleteBook_WhenNotValidID_ThrowNotFoundException()
    {
        // Arrange
        const int bookId = 10;
        var handler = new DeleteBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new DeleteBook.Command(bookId);
        var count = await _bookRepository.Object.GetCouuntAsync(CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

        Assert.Equal(6, count);
    }
}
