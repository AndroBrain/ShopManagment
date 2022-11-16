using ShopManagmentAPI.domain.model.shop;

namespace ShopManagmentAPI.domain.repository;

public interface IShopRepository
{
    public void Create(ShopDto shop);
    public List<ShopDto> GetAll(int ownerId);
    public bool Update(ShopDto shop);
    public bool Delete(int id);
}
