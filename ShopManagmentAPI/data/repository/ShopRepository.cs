using ShopManagmentAPI.data.db.shop;
using ShopManagmentAPI.data.mappers;
using ShopManagmentAPI.domain.model.shop;
using ShopManagmentAPI.domain.repository;

namespace ShopManagmentAPI.data.repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly IShopDao shopDao;
        public ShopRepository(IShopDao shopDao)
        {
            this.shopDao = shopDao;
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

        public bool Update(ShopDto shop)
        {
            return shopDao.Update(ShopMappers.DtoToEntity(shop));
        }
    }
}
