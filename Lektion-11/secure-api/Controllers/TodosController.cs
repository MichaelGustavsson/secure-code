using Microsoft.AspNetCore.Mvc;
using secure_api.ViewModels;

namespace secure_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        [HttpPost]
        public ActionResult CreateTodo(TodoItemPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mappar data ifrån viewmodel till model entitet...

            return StatusCode(201);
        }
    }
}
