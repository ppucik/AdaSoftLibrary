using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdaSoftLibrary.Web.Controllers
{
    public class BookController : Controller
    {
        public const string NAME = "Book";
        public const string ACTION_INDEX = nameof(Index);

        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public BookController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        //[Authorize]
        public async Task<IActionResult> Index(string? searchTerm = null, bool onlyAvailable = false)
        {
            var model = new BookViewModel
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false
            };

            var query = new GetBooks.Query();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.Search = new SearchViewModel
                {
                    SearchTerm = searchTerm,
                    OnlyAvailable = onlyAvailable
                };

                query.SearchTerm = searchTerm;
                query.Borrowed = onlyAvailable ? false : null;
            }

            model.Books = await _mediator.Send(query);

            return View(model);
        }

        // Add other action methods
    }
}
