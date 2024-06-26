using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdaSoftLibrary.Web.Controllers;

public class AuthorController : Controller
{
    public const string NAME = "Author";
    public const string ACTION_INDEX = nameof(Index);

    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;

    public AuthorController(ILogger<HomeController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IActionResult> Index(AuthorsViewModel model)
    {
        var query = new GetAuthors.Query
        {
            SearchAuthor = model.SearchAuthor
        };

        model.Authors = await _mediator.Send(query);

        return View(model);
    }
}
