using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace BooksMvcClient.Controllers;

public class AboutController : Controller
{
    // 
    // GET: /About/
        public IActionResult Index()
    {
        // This is a simple dockerized ASP.NET MVC application that retrieves the books from the API.
        ViewData["Version"] = "0.0.1";
        ViewData["Author"] = "Marcelo Gomes Marque";
        ViewData["Created"] = "2024-04-10";
        ViewData["Updated"] = "2024-04-10";
        return View();
    }
}