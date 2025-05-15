using Microsoft.AspNetCore.Mvc;

namespace dotnetXSS.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
