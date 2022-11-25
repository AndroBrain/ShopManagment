using ShopManagmentAPI.domain.model.product;

namespace ShopManagmentAPI.domain.repository
{
    public interface IProductRepository
    {
        public void Create(ProductDto shop);
        public List<ProductDto> GetAll(int ownerId);
        public List<ProductDto> GetByShop(int shopId);
        public bool Update(ProductDto shop);
        public bool Delete(int id);
    }
}
