using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain.model.user;

namespace ShopManagmentAPI.data.db.user;

public interface IUserDao
{
    public List<UserEntity> GetAll();
    public UserEntity? Get(string email);
    public UserEntity? Create(UserEntity user);
    public void Update(UserEntity user);
    public bool Delete(string email);
}
