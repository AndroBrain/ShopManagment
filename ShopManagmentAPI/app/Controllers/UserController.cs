using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopManagmentAPI.data.db;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain;
using ShopManagmentAPI.domain.model.user;
using ShopManagmentAPI.domain.repository;
using ShopManagmentAPI.domain.service.user;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopManagmentAPI.app.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService = new UserService();
    private readonly IUserRepository userRepository = new UserRepository(new UserDb());
    private readonly IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

    [HttpPost("Register")]
    public ActionResult RegisterUser([FromBody] RegisterUserDTO user)
    {
        try
        {
            var newUser = new User()
            {
                Email = user.Email,
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

    [HttpPost("Login")]
    public ActionResult Login([FromBody] LoginDTO loginDTO)
    {
        var user = userRepository.GetByEmail(loginDTO.Email);
        if (user == null)
        {
            return Forbid("User with given email doesn't exist");
        }
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            return Forbid("Invalid password");
        }

        return Ok(userService.generateJWT(user));
    }
}
