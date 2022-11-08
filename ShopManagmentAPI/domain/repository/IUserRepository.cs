using ShopManagmentAPI.domain.model;

namespace ShopManagmentAPI.domain.repository
{
    public interface IUserRepository
    {
        public User AddUser(User user);
        // TODO Figure out if there is a need for changing it since GettingUser reuires authentication
        public User GetUserById(int id);
        public User UpdateUser(User user);
        public User DeleteUser(int id);
    }
}
