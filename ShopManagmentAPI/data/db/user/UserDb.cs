using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Diagnostics;

namespace ShopManagmentAPI.data.db.user;

public class UserDb : IUserDao
{
    public UserDb()
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            conn.CreateTable<UserEntity>();
            conn.CreateTable<UserRoleEntity>();
            conn.CreateTable<ShopEntity>();
            conn.CreateTable<ProductEntity>();
            conn.CreateTable<ShopProductEntity>();
        }
    }

    public List<UserEntity> GetAll()
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.GetAllWithChildren<UserEntity>().ToList();
        }
    }

    public UserEntity? Get(string email)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            try
            {
                var users = conn.GetAllWithChildren<UserEntity>(filter: u => u.Email == email);
                return users.FirstOrDefault(defaultValue: null);
            } catch(InvalidOperationException e)
            {
                return null;
            }
        }
    }
    public UserEntity? Create(UserEntity user)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            try
            {
                Console.WriteLine("Adding user " + user.Name);
                conn.Insert(user);
                conn.Insert(user.Role);
                conn.UpdateWithChildren(user);
                return user;
            }  catch(SQLiteException e)
            {
                return null;
            }
        }
    }

    public bool Update(string actualEmail, UserEntity user)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            var actualUser = Get(actualEmail);
            if (actualUser == null) return false;
            user.Id = actualUser.Id;
            if (user.RoleId == 0)
            {
                user.RoleId = actualUser.RoleId;
            }
            try
            {
                conn.Update(user);
                return true;
            } catch(SQLiteException e)
            {
                return false;
            }
        }
    }

    public bool Delete(string email)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            var userEntity = Get(email);
            if (userEntity is null) return false;
            var result = conn.Delete(userEntity) > 0;
            conn.UpdateWithChildren(userEntity);
            return result;
        }
    }
}
