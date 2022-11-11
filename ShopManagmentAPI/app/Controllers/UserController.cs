using Microsoft.AspNetCore.Mvc;
using ShopManagmentAPI.data.db;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain.model;
using ShopManagmentAPI.domain.repository;

namespace ShopManagmentAPI.app.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository = new UserRepository(new UserDb());

    [HttpPost("RegisterUser")]
    public ActionResult<User> RegisterUser([FromBody] User user)
    {
        try
        {
            userRepository.Create(user);
            return Ok("Successfully added new user");
        } catch(ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}
