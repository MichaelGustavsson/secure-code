using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using secure_api.ViewModels;

namespace secure_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly HtmlSanitizer _htmlSanitizer = new();

        [HttpGet]
        public ActionResult ListAllTodos()
        {
            return Ok(new { success = true, data = "Det funkar" });
        }

        [HttpPost]
        public ActionResult CreateTodo([FromBody]TodoItemPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kontrollera eller städa bort farliga element...
            model.Title = _htmlSanitizer.Sanitize(model.Title);
            model.Description = _htmlSanitizer.Sanitize(model.Description);

            if (model.Title.Trim().Length == 0)
            {
                return BadRequest("Titel måste anges");
            }

            // Mappar data ifrån viewmodel till model entitet...

            return Ok(new { success = true, data = model });
        }
    }
}
