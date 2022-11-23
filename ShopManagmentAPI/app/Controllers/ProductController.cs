
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManagmentAPI.data.db.product;
using ShopManagmentAPI.data.db.shop;
using ShopManagmentAPI.data.db.user;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain.model.product;
using ShopManagmentAPI.domain.model.shop;
using ShopManagmentAPI.domain.repository;
using ShopManagmentAPI.domain.service.email;
using ShopManagmentAPI.domain.service.user;

namespace ShopManagmentAPI.app.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IUserRepository userRepository = new UserRepository(new UserDb());
    private readonly IAuthenticationService authService;
    private readonly IProductRepository productRepository = new ProductRepository(new ProductDb());

    public ProductController()
    {
        authService = new AuthenticationService(userRepository, new EmailSender());
    }

    [HttpPost("Create")]
    public ActionResult Create([FromBody] CreateProductDto product)
    {
        var owner = authService.GetUserFromToken(HttpContext);
        if (owner is null) return Unauthorized();
        productRepository.Create(new ProductDto()
        {
            Name = product.Name,
        });
        return Ok();
    }

    [HttpPut("Update")]
    public ActionResult Update([FromBody] UpdateProductDto product)
    {
        var owner = authService.GetUserFromToken(HttpContext);
        if (owner is null) return Unauthorized();
        var result = productRepository.Update(new ProductDto()
        {
            Id = product.Id,
            Name = product.Name,
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
        var result = productRepository.Delete(shopId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

}