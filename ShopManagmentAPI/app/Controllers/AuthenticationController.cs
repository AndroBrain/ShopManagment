using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManagmentAPI.data.db;
using ShopManagmentAPI.data.db.user;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain.model.authentication;
using ShopManagmentAPI.domain.service.email;
using AuthenticationService = ShopManagmentAPI.domain.service.user.AuthenticationService;
using IAuthenticationService = ShopManagmentAPI.domain.service.user.IAuthenticationService;

namespace ShopManagmentAPI.app.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService userService = new AuthenticationService(new UserRepository(new UserDb()), new EmailSender());
    private readonly ILogger<AuthenticationController> logger;

    public AuthenticationController(ILogger<AuthenticationController> logger)
    {
        this.logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public ActionResult RegisterUser([FromBody] RegisterDto user)
    {
        try
        {
            userService.RegisterUser(user);
            return Ok("Successfully added new user");
        }
        catch (ArgumentException e)
        {
            logger.LogError(e.Message);
            return BadRequest("User already exists");
        }
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public ActionResult Login([FromBody] LoginDTO loginDTO)
    {
        var user = userService.FindUserByEmail(loginDTO.Email);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status403Forbidden, "Invalid Email");
        }

        var passwordVerificationResult = userService.VerifyPasswordHashes(user, loginDTO.Password);
        if (!passwordVerificationResult)
        {
            return StatusCode(StatusCodes.Status403Forbidden, "Invalid Password");
        }

        return Ok(userService.GenerateJWT(user));
    }
}
