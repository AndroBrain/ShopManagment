using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopManagmentAPI.data.db;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain.model.user;
using ShopManagmentAPI.domain.repository;

namespace ShopManagmentAPI.app.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository = new UserRepository(new UserDb());
    private readonly IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

    [HttpPost("RegisterUser")]
    public ActionResult<User> RegisterUser([FromBody] RegisterUserDTO user)
    {
        try
        {
            var newUser = new User()
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role,
            };
            newUser.PasswordHash = passwordHasher.HashPassword(newUser, user.Password);
            userRepository.Create(newUser);
            return Ok("Successfully added new user");
        } catch(ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}
