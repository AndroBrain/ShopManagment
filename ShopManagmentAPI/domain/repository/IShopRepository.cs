using ShopManagmentAPI.domain.model;

namespace ShopManagmentAPI.domain.repository
{
    public interface IShopRepository
    {
        public Shop AddShop(Shop shop);
        public Shop GetShop(int id);
        public List<Product> GetShopProducts(int id);
        public Shop UpdateShop(Shop shop);
        public Shop DeleteShop(int userId, int id);
    }
}
