using ShopManagmentAPI.domain.model.shop;

namespace ShopManagmentAPI.domain.model.product;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ShopDto> Shops { get; set; } = new List<ShopDto>();
}
