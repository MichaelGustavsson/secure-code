using Microsoft.AspNetCore.Mvc;

namespace dotnetXSS.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        Response.Cookies.Append("TheSuperSecret", "Detta är hemligt!!!");
        return View();
    }

    [HttpGet()]
    public IActionResult FindVehicle(string regNumber)
    {
        // Fejka att vi går till en databas och söker upp en bil med inskickat regnummer...
        var model = $"The vehicle with registrationnumber <i>{regNumber}</i> is a Volvo V70 2014";

        // Response.ContentType = "text/plain";
        Response.ContentType = "text/html";
        return Content(model);
    }

    [HttpGet()]
    public IActionResult FindVehicleWithView(string regNumber)
    {
        var model = $"The vehicle with registrationnumber <i>{regNumber}</i> is a Volvo V70 2014";
        return View("VehicleDetails", model);
    }
}
