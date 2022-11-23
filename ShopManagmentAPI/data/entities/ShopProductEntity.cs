using SQLiteNetExtensions.Attributes;

namespace ShopManagmentAPI.data.entities;

public class ShopProductEntity
{
    [ForeignKey(typeof(ShopEntity))]
    public int ShopId { get; set; }
    [ForeignKey(typeof(ProductEntity))]
    public int ProductId { get; set; }
}
