using ShopManagmentAPI.domain.model.shop;

namespace ShopManagmentAPI.domain.model.product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public List<UpdateShopDto> Shops { get; set; }
    }
}
