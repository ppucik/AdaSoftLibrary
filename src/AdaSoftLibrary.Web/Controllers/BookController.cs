using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Web.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaSoftLibrary.Web.Controllers
{
    public class BookController : Controller
    {
        #region Constants
        public const string NAME = "Book";
        public const string ACTION_INDEX = nameof(Index);
        public const string ACTION_CREATE = nameof(Create);
        public const string ACTION_EDIT = nameof(Edit);
        public const string ACTION_DELETE = nameof(Delete);
        #endregion

        #region Constructor

        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly INotyfService _notyf;

        public BookController(
            ILogger<HomeController> logger,
            IMediator mediator,
            INotyfService notyf)
        {
            _logger = logger;
            _mediator = mediator;
            _notyf = notyf;
        }

        #endregion

        #region Index, Detail

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

            var query = new GetBook.Query(id);

            model.Book = await _mediator.Send(query);

            return View(model);
        }

        #endregion

        #region Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            // Frontend validation
            if (ModelState.IsValid)
            {
                var command = new CreateBook.Command
                {
                    Author = model.Author,
                    Name = model.Name,
                    Year = model.Year,
                    Description = model.Description
                };

                var result = await _mediator.Send(command);

                if (result.Success)
                {
                    // Success toastr alert
                    _notyf.Success("Kniha bola úspešne zaevidovaná!");
                    return RedirectToAction(ACTION_INDEX);
                }
                else
                {
                    // Backhand error message
                    _notyf.Error(result.Message);
                    TempData["msg"] = string.Join(", ", result.ValidationErrors ?? []);
                }
            }

            return View();
        }

        #endregion

        #region Edit

        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var query = new GetBook.Query(id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            return View(model);
        }

        #endregion

        #region Delete

        [Authorize]
        public IActionResult Delete()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var query = new GetBook.Query(id);
            var book = await _mediator.Send(query);

            var model = new DeleteViewModel
            {
                Author = book.Author,
                Name = book.Name
            };

            return View(model);
        }

        #endregion
    }
}
