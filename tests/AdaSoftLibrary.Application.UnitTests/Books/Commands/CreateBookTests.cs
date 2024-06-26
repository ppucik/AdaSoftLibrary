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
    public async Task CreateBook_CommandHandler_WhenValidQuery()
    {
        // Arrange
        var handler = new CreateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new CreateBook.Command
        {
            Author = "Peter Púčik",
            Name = "Test novej knihy",
            Year = 2024,
            Description = "Krátky popis"
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<CreateBook.Response>();

        Assert.True(result.Success);
        Assert.Null(result.Message);
        Assert.Null(result.ValidationErrors);
        Assert.Equal(result.Data?.Id, 7);

        // pocet knih = 7
    }
}
