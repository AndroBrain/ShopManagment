using ShopManagmentAPI.domain.model.product;
using ShopManagmentAPI.domain.model.shop;

namespace ShopManagmentAPI.domain.repository;

public interface IShopRepository
{
    public void Create(ShopDto shop);
    public List<ShopDto> GetAll(int ownerId);
    public List<ShopDto> GetByProduct(int productId);
    public bool Update(ShopDto shop);
    public bool AddProduct(AddProductToShopDto addProductToShopDto);
    public bool Delete(int id);
}
