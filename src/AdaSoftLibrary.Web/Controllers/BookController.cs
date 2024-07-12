using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Domain.Enums;
using AdaSoftLibrary.Web.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaSoftLibrary.Web.Controllers;

public class BookController : Controller
{
    #region Constants
    public const string NAME = "Book";
    public const string ACTION_INDEX = nameof(Index);
    public const string ACTION_DETAIL = nameof(Detail);
    public const string ACTION_CREATE = nameof(Create);
    public const string ACTION_EDIT = nameof(Edit);
    public const string ACTION_BORROW = nameof(Borrow);
    public const string ACTION_RETURN = nameof(Return);
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

    #region Index

    public async Task<IActionResult> Index(SearchViewModel search)
    {
        if (search.OnlyAvailable)
        {
            search.BookFilter = BookFilterEnum.FreeBooks;
        }

        var model = new BooksViewModel
        {
            Search = search,
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false
        };

        var query = new GetBooks.Query
        {
            BookFilter = search.BookFilter,
            Author = search.Author,
            Name = search.Name,
            SearchTerm = search.SearchTerm
        };

        model.Books = await _mediator.Send(query);

        return View(model);
    }

    #endregion

    #region Detail

    public async Task<IActionResult> Detail(int id)
    {
        var query = new GetBook.Query(id);
        var book = await _mediator.Send(query);

        if (book is null)
        {
            return NotFound();
        }

        var model = new DetailViewModel
        {
            Id = id,

            Author = book.Author,
            Name = book.Name,
            Year = book.Year,
            Description = book.Description,

            FirstName = book.FirstName,
            LastName = book.LastName,
            FromDate = book.BorrowedFrom,

            IsBorrowed = book.IsBorrowed,
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false
        };

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
        if (!ModelState.IsValid)
        {
            return View();
        }

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
            _notyf.Success("Kniha bola zaevidovaná");
            _logger.LogInformation($"Kniha {model.Author}:{model.Name} bola zaevidovaná");

            return RedirectToAction(ACTION_INDEX);
        }
        else
        {
            // Backhand error message
            _notyf.Error(result.Message);
            _logger.LogError($"Kniha {model.Author}:{model.Name} nebola zaevidovaná");
            TempData["msg"] = result.ValidationErrorsSummary;
        }

        return View();
    }

    #endregion

    #region Edit

    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var query = new GetBook.Query(id);
        var book = await _mediator.Send(query);

        if (book is null)
        {
            return NotFound();
        }

        var model = new EditViewModel
        {
            Id = id,
            Author = book.Author,
            Name = book.Name,
            Year = book.Year,
            Description = book.Description,

            FirstName = book.FirstName,
            LastName = book.LastName,
            FromDate = book.BorrowedFrom,
            IsBorrowed = book.IsBorrowed
        };

        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditViewModel model)
    {
        // Frontend validation
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var command = new UpdateBook.Command
        {
            Id = model.Id,
            Author = model.Author,
            Name = model.Name,
            Year = model.Year,
            Description = model.Description,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BorrowedFrom = model.FromDate
        };

        var result = await _mediator.Send(command);

        if (result.Success)
        {
            // Success toastr alert
            _notyf.Success("Kniha bola uložená");
            _logger.LogInformation($"Kniha ({model.Id}) {model.Author}:{model.Name} bola uložená");

            return RedirectToAction(ACTION_INDEX);
        }
        else
        {
            // Backhand error message
            _notyf.Error(result.ValidationErrorsSummary);
            _logger.LogError($"Kniha ({model.Id}) {model.Author}:{model.Name} nebola uložená");
            TempData["msg"] = result.ValidationErrorsSummary;
        }

        return View(model);
    }

    #endregion

    #region Borrow

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Borrow(DetailViewModel model)
    {
        // Frontend validation
        if (!ModelState.IsValid)
        {
            return View(ACTION_DETAIL, model);
        }

        var command = new BorrowBook.Command
        {
            Id = model.Id,
            FirstName = model.FirstName!,
            LastName = model.LastName!
        };

        var result = await _mediator.Send(command);

        if (result.Success)
        {
            // Success toastr alert
            _notyf.Success("Kniha bola objednaná");
            _logger.LogInformation($"Kniha ({model.Id}) {model.Author}:{model.Name} bola objednaná");

            return RedirectToAction(ACTION_INDEX);
        }
        else
        {
            // Backhand error message
            _notyf.Error(result.ValidationErrorsSummary);
            _logger.LogError($"Kniha ({model.Id}) {model.Author}:{model.Name} nebola objednaná");
        }

        return View(ACTION_DETAIL, model);
    }

    #endregion

    #region Return

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Return(DetailViewModel model)
    {
        var command = new ReturnBook.Command(model.Id);
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            // Success toastr alert
            _notyf.Success("Kniha bola vrátená");
            _logger.LogInformation($"Kniha ({model.Id}) {model.Author}:{model.Name} bola vrátená");

            return RedirectToAction(ACTION_INDEX);
        }
        else
        {
            // Backhand error message
            _notyf.Error(result.ValidationErrorsSummary);
            _logger.LogError($"Kniha ({model.Id}) {model.Author}:{model.Name} nebola vrátená");
        }

        return View(ACTION_DETAIL, model);
    }

    #endregion

    #region Delete

    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var query = new GetBook.Query(id);
        var book = await _mediator.Send(query);

        if (book is null)
        {
            return NotFound();
        }

        var model = new DeleteViewModel
        {
            Author = book.Author,
            Name = book.Name
        };

        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(DeleteViewModel model)
    {
        var comand = new DeleteBook.Command(model.Id);
        var result = await _mediator.Send(comand);

        // Success toastr alert
        _notyf.Success("Kniha bola vymazaná");
        _logger.LogInformation($"Kniha ({model.Id}) {model.Author}:{model.Name} bola vymazaná");

        return RedirectToAction(ACTION_INDEX);
    }

    #endregion
}
