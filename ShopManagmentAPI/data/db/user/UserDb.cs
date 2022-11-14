using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace ShopManagmentAPI.data.db.user;

public class UserDb : IUserDao
{
    public UserDb()
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            conn.CreateTable<UserEntity>();
            conn.CreateTable<UserRoleEntity>();
        }
    }

    public List<UserEntity> GetAll()
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.Table<UserEntity>().ToList();
        }
    }

    public UserEntity? Get(string email)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            try
            {
                return conn.GetWithChildren<UserEntity>(email);
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

    public void Update(UserEntity user)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            conn.UpdateWithChildren(user);
        }
    }

    public bool Delete(string email)
    {
        using (SQLiteConnection conn = new SQLiteConnection(DbSettings.dbPath))
        {
            return conn.Delete(email) > 0;
        }
    }
}
