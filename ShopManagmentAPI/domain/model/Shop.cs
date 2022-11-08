using ShopManagmentAPI.data.entities;

namespace ShopManagmentAPI.domain.model
{
    public class Shop
    {
        public string Name { get; set; }
        public List<ProductEntity> Products { get; set; }
    }
}
