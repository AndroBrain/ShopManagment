using ShopManagmentAPI.domain.model.user;

namespace ShopManagmentAPI.domain.repository
{
    public interface IUserRepository
    {
        public User Create(User user);
        public User? GetByEmail(string email);
        public List<User> GetAll();
        public User Update(User user);
        public bool Delete(string email);
    }
}
