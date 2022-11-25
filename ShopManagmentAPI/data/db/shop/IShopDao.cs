using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain.model.shop;

namespace ShopManagmentAPI.data.db.shop;

public interface IShopDao
{
    public void Create(ShopEntity shop);
    public List<ShopEntity> GetAll(int ownerId);
    public ShopEntity? Get(int id);
    public bool Update(ShopEntity shop);
    public bool Delete(int id);
}
