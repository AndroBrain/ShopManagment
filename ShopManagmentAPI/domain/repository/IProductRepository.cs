using ShopManagmentAPI.domain.model;

namespace ShopManagmentAPI.domain.repository
{
    public interface IProductRepository
    {
        public Product AddProduct(Product product);
        public Product GetProduct(int id);
        public List<Shop> GetShopsOfProduct(int id);
        public Product UpdateProduct(Product product);
        public Product DeleteProduct(int id);
    }
}
