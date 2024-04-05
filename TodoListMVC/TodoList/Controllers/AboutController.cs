using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class AboutController : Controller
{
    // 
    // GET: /About/
        public IActionResult Index()
    {
        // This is a simple to-do list application following the MVC pattern.
        ViewData["Version"] = "0.0.1";
        ViewData["Author"] = "Marcelo Gomes Marque";
        ViewData["Created"] = "2024-04-05";
        ViewData["Updated"] = "2024-04-05";
        return View();
    }
}