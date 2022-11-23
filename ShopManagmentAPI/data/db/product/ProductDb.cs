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
            conn.Insert(product);
            product.Shops.ForEach(shop =>
            {
                conn.Insert(shop);
            });
            conn.UpdateWithChildren(product);
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
