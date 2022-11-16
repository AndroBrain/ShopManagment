using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace ShopManagmentAPI.data.db.shop;

public class ShopDb : IShopDao
{
    public void Create(ShopEntity shop)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            var owner = conn.GetWithChildren<UserEntity>(shop.OwnerId);
            conn.Insert(shop);
            owner.Shops.Add(shop);
            conn.UpdateWithChildren(owner);
        }
    }

    public bool Delete(int id)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.Delete<ShopEntity>(id) > 0;
        }
    }

    public List<ShopEntity> GetAll(int ownerId)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.Table<ShopEntity>().Where(s => s.OwnerId == ownerId).ToList();
        }
    }

    public bool Update(ShopEntity shop)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.Update(shop) > 0;
        }
    }
}
