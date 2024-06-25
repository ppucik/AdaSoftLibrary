using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Books.Mapping;
using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.UnitTests.Mocks;
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
    public async Task UpdateBook_CommandHandler_WhenValidQuery()
    {
        // Arrange
        var handler = new UpdateBook.CommandHandler(_bookRepository.Object, _mapper);

        // Act
        var command = new UpdateBook.Command
        {
            Id = 1,
            Author = "Ernest Hemingway",
            Name = "Starec a more",
            Year = 1952,
            Description = "UnitTest",
            FirstName = "Peter",
            LastName = "Prvý",
            BorrowedFrom = new DateOnly(2016, 3, 25)
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<UpdateBook.Response>();

        Assert.True(result.Success);
        Assert.Null(result.Message);
        Assert.Null(result.ValidationErrors);

        //Assert.Equal(result.Data?.Author, "Ernest Hemingway");
        //Assert.Equal(result.Data?.Name, "Starec a more");
        //Assert.Equal(result.Data?.Year, 1952);
        //Assert.Equal(result.Data?.Description, "UnitTest");
        //Assert.Equal(result.Data?.FirstName, "Peter");
        //Assert.Equal(result.Data?.LastName, "Prvý");
        //Assert.Equal(result.Data?.BorrowedFrom, new DateOnly(2016, 3, 25));
    }
}
