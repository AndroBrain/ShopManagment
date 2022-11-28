using ShopManagmentAPI.data.db.product;
using ShopManagmentAPI.data.db.shop;
using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.data.mappers;
using ShopManagmentAPI.domain.model.product;
using ShopManagmentAPI.domain.model.shop;
using ShopManagmentAPI.domain.repository;

namespace ShopManagmentAPI.data.repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductDao productDao;
        private readonly IShopDao shopDao;
        public ProductRepository(IProductDao productDao, IShopDao shopDao)
        {
            this.productDao = productDao;
            this.shopDao = shopDao; 
        }

        public int Create(ProductDto product)
        {
            return productDao.Create(ProductMappers.DtoToEntity(product));
        }

        public List<ProductDto> GetAll(int ownerId)
        {
            return productDao.GetAll(ownerId).ConvertAll(s => ProductMappers.EntityToDto(s));
        }
        public List<ProductDto> GetByShop(int shopId)
        {
            var shop = shopDao.Get(shopId);
            if (shop is null)
            {
                return new List<ProductDto>();
            }
            var allProducts = productDao.GetAll(shop.OwnerId);
            IEnumerable<ProductEntity> shopProducts = allProducts.Where(product => shop.Products.Any(shopProduct => shopProduct.Id == product.Id));

            return shopProducts.Select(product => ProductMappers.EntityToDto(product)).ToList();
        }

        public bool Delete(int id)
        {
            return productDao.Delete(id);
        }

        public bool Update(ProductDto product)
        {
            return productDao.Update(ProductMappers.DtoToEntity(product));
        }

    }
}
