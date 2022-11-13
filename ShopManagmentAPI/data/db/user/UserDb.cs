using ShopManagmentAPI.data.entities;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace ShopManagmentAPI.data.db.user;

public class UserDb : IUserDao
{
    private readonly static Dictionary<string, UserEntity> _users = new();
    private readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private readonly string dbName = "users.db";
    private readonly string dbPath;

    public UserDb()
    {
        dbPath = Path.Combine(path, "ShopManagment", dbName);
    }

    public List<UserEntity> GetAll()
    {
        using (SQLiteConnection conn = new SQLiteConnection(dbPath))
        {
            conn.CreateTable<UserEntity>();
            conn.CreateTable<UserRoleEntity>();
            return conn.Table<UserEntity>().ToList();
        }
    }

    public UserEntity? Get(string email)
    {
        using (SQLiteConnection conn = new SQLiteConnection(dbPath))
        {
            conn.CreateTable<UserEntity>();
            conn.CreateTable<UserRoleEntity>();
            try
            {
                return conn.GetWithChildren<UserEntity>(email);
            } catch(InvalidOperationException e)
            {
                return null;
            }
        }
    }
    public UserEntity Create(UserEntity user)
    {
        using (SQLiteConnection conn = new SQLiteConnection(dbPath))
        {
            conn.CreateTable<UserEntity>();
            conn.CreateTable<UserRoleEntity>();
            try
            {
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
        using (SQLiteConnection conn = new SQLiteConnection(dbPath))
        {
            conn.CreateTable<UserEntity>();
            conn.CreateTable<UserRoleEntity>();
            conn.UpdateWithChildren(user);
        }
    }

    public bool Delete(string email)
    {
        using (SQLiteConnection conn = new SQLiteConnection(dbPath))
        {
            conn.CreateTable<UserEntity>();
            conn.CreateTable<UserRoleEntity>();
            return conn.Delete(email) > 0;
        }
    }
}
