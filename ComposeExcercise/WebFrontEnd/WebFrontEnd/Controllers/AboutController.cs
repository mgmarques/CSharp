using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace WebFrontEnd.Controllers;

public class AboutController : Controller
{
    // 
    // GET: /About/
        public IActionResult Index()
    {
        // This is a simple dockerized ASP.NET MVC application that retrieves the books from the API.
        ViewData["Version"] = "0.0.1";
        ViewData["Author"] = "Marcelo Gomes Marque";
        ViewData["Created"] = "2024-04-09";
        ViewData["Updated"] = "2024-04-09";
        return View();
    }
}