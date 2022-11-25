using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain;
using ShopManagmentAPI.domain.model.shop;
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
            ShopTypeEntity? shopType = conn.Table<ShopTypeEntity>().Where(sT => sT.Name == shop.ShopType.Name).FirstOrDefault(shop.ShopType);
            if (shopType == shop.ShopType)
            {
                conn.Insert(shop.ShopType);
            }
            else
            {
                shop.ShopType = shopType;
            }
            conn.Insert(shop);
            owner.Shops.Add(shop);
            conn.UpdateWithChildren(owner);
            conn.UpdateWithChildren(shop);
        }
    }

    public List<ShopEntity> GetAll(int ownerId)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.GetAllWithChildren<ShopEntity>().Where(s => s.OwnerId == ownerId).ToList();
        }
    }

    public bool Update(ShopEntity shop)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            var actualShop = conn.GetWithChildren<ShopEntity>(shop.Id);
            if (actualShop is null) return false;
            conn.Insert(shop.ShopType);
            var result = conn.Update(shop) > 0;
            conn.UpdateWithChildren(shop);
            return result;
        }
    }
    public bool Delete(int id)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            var shop = conn.Get<ShopEntity>(id);
            if (shop is null) return false;
            var result = conn.Delete(shop) > 0;
            conn.UpdateWithChildren(shop);
            return result;
        }
    }

    public ShopEntity? Get(int id)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            try
            {
                return conn.GetWithChildren<ShopEntity?>(id);
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }
    }
}
