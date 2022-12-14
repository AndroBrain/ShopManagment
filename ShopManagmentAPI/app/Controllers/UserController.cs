using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManagmentAPI.data.db;
using ShopManagmentAPI.data.db.user;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain.model.user;
using ShopManagmentAPI.domain.repository;
using ShopManagmentAPI.domain.service.email;
using ShopManagmentAPI.domain.service.user;
using System.Security.Claims;

namespace ShopManagmentAPI.app.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository = new UserRepository(new UserDb());
    private readonly IAuthenticationService authSerivce;

    public UserController()
    {
        authSerivce = new AuthenticationService(userRepository, new EmailSender());
    }

    [HttpGet("GetUserInfo")]
    public ActionResult<UserInfoDto> GetUserInfo() {
        var user = authSerivce.GetUserFromToken(HttpContext)?.User;
        if (user is null)
        {
            return Unauthorized();
        }
        var result = new UserInfoDto()
        {
            Name = user.Name,
            Email = user.Email
        };
        return Ok(result);
    }

    [HttpPut("UpdateUserInfo")]
    public ActionResult UpdateUserInfo([FromBody] UserInfoDto updateUserInfoDto)
    {
        var user = authSerivce.GetUserFromToken(HttpContext)?.User;
        if (user is null)
        {
            return Unauthorized();
        }
        var actualEmail = user.Email;
        user.Name = updateUserInfoDto.Name;
        user.Email = updateUserInfoDto.Email;
        if (userRepository.Update(actualEmail, user))
        {
            return Ok("Ok");
        }
        return BadRequest();
    }

    [HttpDelete("Delete")]
    public ActionResult Delete()
    {
        var user = authSerivce.GetUserFromToken(HttpContext)?.User;
        if (user is null)
        {
            return Unauthorized();
        }
        if (userRepository.Delete(user.Email))
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet("GetAllUsers")]
    [Authorize(Roles = "admin")]
    public ActionResult<List<User>> GetAllUsers()
    {
        return Ok(userRepository.GetAll());
    }

    [HttpPut("UpdateUserInfoByEmail")]
    [Authorize(Roles = "admin")]
    public ActionResult UpdateUserInfoByEmail([FromBody] UpdateUserInfoByEmailDto updateUserInfoByEmailDto)
    {
        var user = userRepository.GetByEmail(updateUserInfoByEmailDto.ActualEmail)?.User;
        if (user is null)
        {
            return NotFound();
        }
        user.Name = updateUserInfoByEmailDto.Name;
        user.Email = updateUserInfoByEmailDto.Email;
        if (userRepository.Update(updateUserInfoByEmailDto.ActualEmail, user))
        {
            return Ok("Ok");
        }
        return BadRequest();
    }
}
