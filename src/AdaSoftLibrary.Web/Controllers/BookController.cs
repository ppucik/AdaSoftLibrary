using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> Index(string? searchTerm = null, bool onlyAvailable = false)
        {
            var model = new BooksViewModel
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

        public async Task<IActionResult> Detail(int id)
        {
            var model = new DetailViewModel
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false
            };

            var query = new GetBook.Query() { Id = id };
            model.Book = await _mediator.Send(query);

            return View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var query = new GetBook.Query() { Id = id };
            var book = await _mediator.Send(query);

            var model = new EditViewModel
            {
                Author = book.Author,
                Name = book.Name,
                Year = book.Year,
                Description = book.Description,
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var query = new GetBook.Query() { Id = id };
            var book = await _mediator.Send(query);

            var model = new DeleteViewModel
            {
                Author = book.Author,
                Name = book.Name
            };

            return View(model);
        }
    }
}
