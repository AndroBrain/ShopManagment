using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace ShopManagmentAPI.data.db.product;

public class ProductDb : IProductDao
{
    public void Create(ProductEntity product)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            var owner = conn.GetWithChildren<UserEntity>(product.OwnerId);
            conn.Insert(product);
            owner.Products.Add(product);
            conn.UpdateWithChildren(owner);
        }
    }

    public List<ProductEntity> GetAll(int ownerId)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.Table<ProductEntity>().Where(s => s.OwnerId == ownerId).ToList();
        }
    }

    public bool Delete(int id)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.Delete<ProductEntity>(id) > 0;
        }
    }

    public bool Update(ProductEntity product)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.Update(product) > 0;
        }
    }
}
