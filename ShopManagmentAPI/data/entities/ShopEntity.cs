using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ShopManagmentAPI.data.entities;
[Table("Shops")]
public class ShopEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    [ForeignKey(typeof(UserEntity))]
    public int OwnerId { get; set; }
    [ManyToOne]
    public UserEntity User { get; set; }
}
