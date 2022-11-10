using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain.model;

namespace ShopManagmentAPI.data.mappers;

public class UserMapper
{
    public static UserEntity ModelToEntity(User user)
    {
        var entity = new UserEntity();
        entity.Id = user.Id;
        entity.Name = user.Name;
        entity.Role = user.Role;
        return entity;
    }

    public static User EntityToModel(UserEntity entity)
    {
        var user = new User();
        user.Id = entity.Id;
        user.Name = entity.Name;
        user.Role = entity.Role;
        return user;
    }
}
