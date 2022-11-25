using ShopManagmentAPI.data.db.product;
using ShopManagmentAPI.data.db.shop;
using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.data.mappers;
using ShopManagmentAPI.domain.model.shop;
using ShopManagmentAPI.domain.repository;

namespace ShopManagmentAPI.data.repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly IShopDao shopDao;
        private readonly IProductDao productDao;
        public ShopRepository(IShopDao shopDao, IProductDao productDao)
        {
            this.shopDao = shopDao;
            this.productDao = productDao;
        }

        public bool AddProduct(AddProductToShopDto addProductToShopDto)
        {
            var shop = shopDao.Get(addProductToShopDto.ShopId);
            if (shop is null) return false;
            var product = productDao.Get(addProductToShopDto.ProductId);
            if (product is null) return false;
            shop.Products.Add(product);
            product.Shops.Add(shop);
            productDao.Update(product);
            return shopDao.Update(shop);
        }

        public void Create(ShopDto shop)
        {
            shopDao.Create(ShopMappers.DtoToEntity(shop));
        }

        public bool Delete(int id)
        {
            return shopDao.Delete(id);
        }

        public List<ShopDto> GetAll(int ownerId)
        {
            return shopDao.GetAll(ownerId).ConvertAll(s => ShopMappers.EntityToDto(s));
        }

        public List<ShopDto> GetByProduct(int productId)
        {
            var product = productDao.Get(productId);
            if (product is null)
            {
                return new List<ShopDto>();
            }
            var allShops = shopDao.GetAll(product.OwnerId);
            IEnumerable<ShopEntity> productShops = allShops.Where(shop => product.Shops.Any(productShop => productShop.Id == shop.Id));

            return productShops.Select(shop => ShopMappers.EntityToDto(shop)).ToList();
        }

        public bool Update(ShopDto shop)
        {
            return shopDao.Update(ShopMappers.DtoToEntity(shop));
        }
    }
}
