using ShopManagmentAPI.data.entities;

namespace ShopManagmentAPI.data.db.product;

public interface IProductDao
{
    public void Create(ProductEntity product);
    public List<ProductEntity> GetAll(int ownerId);
    public ProductEntity? Get(int id);
    public bool Update(ProductEntity product);
    public bool Delete(int id);
}
