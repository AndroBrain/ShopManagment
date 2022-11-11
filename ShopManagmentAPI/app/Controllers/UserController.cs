using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService userService = new UserService();
    private readonly IUserRepository userRepository = new UserRepository(new UserDb());
    private readonly IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpPost("Login")]
    public ActionResult Login([FromBody] LoginDTO loginDTO)
    {
        var user = userRepository.GetByEmail(loginDTO.Email);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status403Forbidden, "Invalid Email");
        }
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            return StatusCode(StatusCodes.Status403Forbidden, "Invalid password");
        }

        return Ok(userService.generateJWT(user));
    }

    [HttpPost("ChangeName")]
    public ActionResult ChangeName(string Name)
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

    private User? getUserFromToken()
    {
        var email = HttpContext.User.Identities.First()?.Claims?.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
        if (email == null)
            return null;
        return userRepository.GetByEmail(email);
    }
}
