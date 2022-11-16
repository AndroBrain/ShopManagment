using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain.model.shop;

namespace ShopManagmentAPI.data.mappers;

public class ShopMappers
{
    public static ShopDto EntityToDto(ShopEntity entity)
    {
        return new ShopDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            OwnerId = entity.OwnerId,
        };
    }

    public static ShopEntity DtoToEntity(ShopDto shop)
    {
        return new ShopEntity()
        {
            Id = shop.Id,
            Name = shop.Name,
            OwnerId = shop.OwnerId,
        };
    }
}
