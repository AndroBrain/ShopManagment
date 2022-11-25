using ShopManagmentAPI.data.db.product;
using ShopManagmentAPI.data.db.shop;
using ShopManagmentAPI.data.mappers;
using ShopManagmentAPI.domain.model.product;
using ShopManagmentAPI.domain.model.shop;
using ShopManagmentAPI.domain.repository;

namespace ShopManagmentAPI.data.repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductDao productDao;
        public ProductRepository(IProductDao productDao)
        {
            this.productDao = productDao;
        }

        public void Create(ProductDto product)
        {
            productDao.Create(ProductMappers.DtoToEntity(product));
        }

        public List<ProductDto> GetAll(int ownerId)
        {
            return productDao.GetAll(ownerId).ConvertAll(s => ProductMappers.EntityToDto(s));
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
