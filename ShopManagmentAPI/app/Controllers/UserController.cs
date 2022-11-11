using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManagmentAPI.data.db;
using ShopManagmentAPI.data.db.user;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain.model.user;
using ShopManagmentAPI.domain.repository;
using System.Security.Claims;

namespace ShopManagmentAPI.app.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository = new UserRepository(new UserDb());

    [HttpPost("ChangeName")]
    public ActionResult ChangeName([FromQuery] string Name)
    {
        var user = getUserFromToken();
        if (user is null)
        {
            return Unauthorized();
        }
        user.Name = Name;
        userRepository.Update(user);
        return Ok("OK");
    }

    [HttpPost("ChangeAnyName")]
    [Authorize(Roles = "admin")]
    public ActionResult ChangeAnyName([FromBody] ChangeAnyNameDto changeAnyNameDto)
    {
        var user = userRepository.GetByEmail(changeAnyNameDto.Email);
        if (user is null)
        {
            return NotFound();
        }
        user.Name = changeAnyNameDto.Name;
        var updatedUser = userRepository.Update(user);
        return Ok(updatedUser.Name);
    }

    private User? getUserFromToken()
    {
        var email = HttpContext.User.Identities.First()?.Claims?.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
        if (email == null)
            return null;
        return userRepository.GetByEmail(email);
    }
}
