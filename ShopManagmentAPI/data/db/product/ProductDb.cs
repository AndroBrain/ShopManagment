using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace ShopManagmentAPI.data.db.product;

public class ProductDb : IProductDao
{
    public int Create(ProductEntity product)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            var owner = conn.GetWithChildren<UserEntity>(product.OwnerId);
            conn.Insert(product);
            owner.Products.Add(product);
            conn.UpdateWithChildren(owner);
            return product.Id;
        }
    }

    public List<ProductEntity> GetAll(int ownerId)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.GetAllWithChildren<ProductEntity>().Where(s => s.OwnerId == ownerId).ToList();
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
            var result = conn.Update(product) > 0;
            conn.UpdateWithChildren(product);
            return result;
        }
    }

    public ProductEntity? Get(int id)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            try
            {
                return conn.GetWithChildren<ProductEntity?>(id, true);
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }
    }
}
