using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Application.UnitTests.Mocks;

namespace AdaSoftLibrary.Application.UnitTests.Books.Queries;

public class GetBookTests
{
    private readonly IAppDataContext _mockAppXmlContext;

    public GetBookTests()
    {
        _mockAppXmlContext = MockAppDataContext.GetAppXmlContext().Object;
    }

    //[Fact]
    public async Task GetBook_WhenValidQuery_ShouldReturnBook()
    {
        // Arrange
        //var query = new GetBook.Query(1);

        // Act
        //var result = await _mediator.Send(query);

        // Assert
        //Assert.NotNull(result);
    }
}