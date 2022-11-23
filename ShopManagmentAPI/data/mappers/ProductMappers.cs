using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain.model.product;
using ShopManagmentAPI.domain.model.shop;

namespace ShopManagmentAPI.data.mappers;

public class ProductMappers
{
    public static ProductDto EntityToDto(ProductEntity entity)
    {
        return new ProductDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Shops = entity.Shops.ConvertAll(s => ShopMappers.EntityToDto(s)),
        };
    }

    public static ProductEntity DtoToEntity(ProductDto product)
    {
        return new ProductEntity()
        {
            Id = product.Id,
            Name = product.Name,
            Shops = product.Shops.ConvertAll(s => ShopMappers.DtoToEntity(s)),
        };
    }
}
