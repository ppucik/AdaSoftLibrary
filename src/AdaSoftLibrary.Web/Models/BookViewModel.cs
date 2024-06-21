using AdaSoftLibrary.Application.Books.Queries;

namespace AdaSoftLibrary.Web.Models;

public class BookViewModel
{
    public IReadOnlyCollection<GetBookResponse> Books { get; set; } = null!;

    public SearchViewModel? Search { get; set; }

    public bool IsAuthenticated { get; set; }
}
