using SQLite;

namespace ShopManagmentAPI.data.entities;
[Table("Roles")]
public class UserRoleEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}
