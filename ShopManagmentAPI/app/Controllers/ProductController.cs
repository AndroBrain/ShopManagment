
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
    private readonly IProductRepository productRepository = new ProductRepository(new ProductDb(), new ShopDb());

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
            OwnerId = owner.Id,
        });
        return Ok();
    }

    [HttpGet("GetAll")]
    public ActionResult<List<ProductDto>> GetAll()
    {
        var user = authService.GetUserFromToken(HttpContext);
        if (user is null) return Unauthorized();
        return productRepository.GetAll(user.Id);
    }

    [HttpGet("GetByShop")]
    public ActionResult<List<ProductDto>> GetByShop([FromQuery] int shopId)
    {
        return Ok(productRepository.GetByShop(shopId));
    }

    [HttpPut("Update")]
    public ActionResult Update([FromBody] UpdateProductDto product)
    {
        var owner = authService.GetUserFromToken(HttpContext);
        if (owner is null) return Unauthorized();
        var result = productRepository.Update(new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            OwnerId = owner.Id,
        });
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpDelete("Delete")]
    public ActionResult Delete([FromQuery] int productId)
    {
        var result = productRepository.Delete(productId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

}