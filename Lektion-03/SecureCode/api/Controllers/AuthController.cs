using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Entities;
using api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<User> userManager, IConfiguration config) : ControllerBase
{
  private readonly UserManager<User> _userManager = userManager;
  private readonly IConfiguration _config = config;

  [HttpPost("register")]
  public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
  {
    try
    {
      var user = new User
      {
        UserName = model.UserName,
        Email = model.UserName,
        FirstName = model.FirstName,
        LastName = model.LastName
      };
      var result = await _userManager.CreateAsync(user, model.Password);

      if (result.Succeeded)
      {
        return StatusCode(201);
      }

      return BadRequest(new { success = false, message = result.Errors });
    }
    catch (Exception ex)
    {
      return BadRequest(new { success = false, message = ex.Message });
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginViewModel model)
  {
    try
    {
      var user = await _userManager.FindByNameAsync(model.UserName);

      if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
      {
        return Unauthorized(new { success = false, message = "Unauthorized" });
      }

      return Ok(new { success = true, email = user.Email, token = CreateToken(user) });
    }
    catch (Exception ex)
    {
      return StatusCode(500, new { success = false, message = ex.Message });
    }
  }


  /* TOKEN SERVICE... */

  private string CreateToken(User user)
  {
    var claims = new List<Claim>
    {
      new(ClaimTypes.Email, user.Email!),
      new(ClaimTypes.Name, user.UserName!),
      new("FirstName", user.FirstName),
      new("LastName",user.LastName)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["tokenSettings:tokenKey"]!));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

    var options = new JwtSecurityToken(
      issuer: null,
      audience: null,
      claims: claims,
      expires: DateTime.Now.AddDays(7),
      signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(options);
  }
}
