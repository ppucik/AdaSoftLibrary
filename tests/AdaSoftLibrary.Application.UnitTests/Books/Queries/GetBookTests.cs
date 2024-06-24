using AdaSoftLibrary.Application.Books.Queries;

namespace AdaSoftLibrary.Application.UnitTests.Books.Queries;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetBookTests(WebAppFactory webAppFactory)
{
    private readonly IMediator _mediator = webAppFactory.CreateMediator();

    //[Fact]
    public async Task GetBook_WhenValidQuery_ShouldReturnBook()
    {
        // Arrange
        var query = new GetBook.Query(1);

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.NotNull(result);
    }
}
