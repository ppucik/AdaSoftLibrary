using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Books.Mapping;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace AdaSoftLibrary.Application.UnitTests.Books.Commands;

/*==========================================================
 * TODO: Nedokoncene, potrebne je este dopracovat !!!
 ==========================================================*/

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
    public async Task DeleteBook_CommandHandler_WhenValidQuery()
    {
        // Arrange
        var handler = new DeleteBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new DeleteBook.Command(1);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<Unit>();

        // pocet knih = 6
    }
}
