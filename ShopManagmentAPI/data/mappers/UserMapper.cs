using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain.model.user;

namespace ShopManagmentAPI.data.mappers;

public class UserMapper
{
    public static UserEntity ModelToEntity(User user)
    {
        var entity = new UserEntity();
        entity.Email = user.Email;
        entity.Name = user.Name;
        entity.PasswordHash = user.PasswordHash;
        entity.Role = new UserRoleEntity()
        {
            Name = user.Role.Name
        };
        return entity;
    }

    public static User EntityToModel(UserEntity entity)
    {
        var user = new User();
        user.Email = entity.Email;
        user.Name = entity.Name;
        user.PasswordHash = entity.PasswordHash;
        user.Role = new UserRole()
        {
            Name = entity.Role.Name
        };
        return user;
    }
}
