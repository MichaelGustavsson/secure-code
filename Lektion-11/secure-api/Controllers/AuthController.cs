using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using secure_api.ViewModels;

namespace secure_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HtmlSanitizer _htmlSanitizer = new();

        // https://localhost:5001/api/auth/login...
        [HttpPost("login")]
        public ActionResult Login([FromBody] UserLoginViewModel model)
        {
            // Först kontrollera så att data är med på korrekt sätt...
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Städa bort icke godkända tecken...
            model.UserName = _htmlSanitizer.Sanitize(model.UserName);
            model.Password = _htmlSanitizer.Sanitize(model.Password);

            if (model.UserName.Trim().Length == 0 || model.Password.Trim().Length == 0)
            {
                return BadRequest("Användarnamn eller lösenord saknas!");
            }

            // Skapa ett token...
            var token = "Ettinteriktigtbratoken";

            // Create cookie...
            Response.Cookies.Append("authCookie", token, new CookieOptions());

            return Ok(new { success = true, data = new { user = model, token = token } });
        }
    }
}
