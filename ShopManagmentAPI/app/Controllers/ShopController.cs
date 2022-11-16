
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManagmentAPI.data.db.shop;
using ShopManagmentAPI.data.db.user;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain.model.shop;
using ShopManagmentAPI.domain.repository;
using ShopManagmentAPI.domain.service.email;
using ShopManagmentAPI.domain.service.user;

namespace ShopManagmentAPI.app.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ShopController : ControllerBase
{
    private readonly IUserRepository userRepository = new UserRepository(new UserDb());
    private readonly IAuthenticationService authService;
    private readonly IShopRepository shopRepository = new ShopRepository(new ShopDb());

    public ShopController()
    {
        authService = new AuthenticationService(userRepository, new EmailSender());
    }

    [HttpPost("Create")]
    public ActionResult Create([FromBody] CreateShopDto shop)
    {
        var owner = authService.GetUserFromToken(HttpContext);
        if (owner is null) return Unauthorized();
        shopRepository.Create(new ShopDto()
        {
            Name = shop.Name,
            OwnerId = owner.Id,
        });
        return Ok();
    }

    [HttpGet("GetAll")]
    public ActionResult<List<ShopDto>> GetAll()
    {
        var user = authService.GetUserFromToken(HttpContext);
        if (user is null) return Unauthorized();
        return shopRepository.GetAll(user.Id);
    }

    [HttpPut("Update")]
    public ActionResult Update([FromBody] UpdateShopDto shop)
    {
        var owner = authService.GetUserFromToken(HttpContext);
        if (owner is null) return Unauthorized();
        var result = shopRepository.Update(new ShopDto()
        {
            Id = shop.Id,
            Name = shop.Name,
            OwnerId = owner.Id,
        });
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpDelete("Delete")]
    public ActionResult Delete([FromQuery] int shopId)
    {
        var result = shopRepository.Delete(shopId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

}