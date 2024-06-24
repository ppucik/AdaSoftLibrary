using Microsoft.AspNetCore.Mvc;

namespace AdaSoftLibrary.Web.Controllers;

public class ReaderController : Controller
{
    public const string NAME = "Author";
    public const string ACTION_INDEX = nameof(Index);

    private readonly ILogger<HomeController> _logger;

    public ReaderController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
