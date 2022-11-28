using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ShopManagmentAPI.data.entities;

public class ProductEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    [ForeignKey(typeof(UserEntity))]
    public int OwnerId { get; set; }
    [ManyToOne]
    public UserEntity User { get; set; }
    [ManyToMany(typeof(ShopProductEntity))]
    public List<ShopEntity> Shops { get; set; }
}
