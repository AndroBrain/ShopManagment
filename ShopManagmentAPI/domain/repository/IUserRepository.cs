using ShopManagmentAPI.domain.model.user;

namespace ShopManagmentAPI.domain.repository
{
    public interface IUserRepository
    {
        public void Create(User user);
        public User? GetByEmail(string email);
        public List<User> GetAll();
        public void Update(User user);
        public bool Delete(string email);
    }
}
