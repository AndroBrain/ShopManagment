using ShopManagmentAPI.data.entities;

namespace ShopManagmentAPI.domain.model
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<ShopEntity> Shops { get; set; }
    }
}
