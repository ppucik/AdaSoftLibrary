using AdaSoftLibrary.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdaSoftLibrary.Web.Controllers;

public class HomeController : Controller
{
    public const string NAME = "Home";
    public const string ACTION_INDEX = nameof(Index);
    public const string ACTION_PRIVACY = nameof(Privacy);
    public const string ACTION_CONTACT = nameof(Contact);
    public const string ACTION_HELP = nameof(Help);

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Help()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
