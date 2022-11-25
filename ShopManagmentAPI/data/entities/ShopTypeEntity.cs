using SQLite;

namespace ShopManagmentAPI.data.entities;
[Table("ShopTypes")]
public class ShopTypeEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}
