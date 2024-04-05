using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using WebApplication1.ViewModels;

namespace MvcMovie.Controllers;

public class HomeController : Controller
{
    [ViewData]
    public string Title { get; set; }

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

    public IActionResult About()
    {
        Title = "About Us";
        ViewData["Message"] = "Your application description page.";

        return View("./About");
    }

    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page.";

        var viewModel = new Address()
        {
            Name = "Microsoft",
            Street = "One Microsoft Way",
            City = "Redmond",
            State = "WA",
            PostalCode = "98052-6399"
        };

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
