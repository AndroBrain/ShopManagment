using ShopManagmentAPI.domain.model;

namespace ShopManagmentAPI.domain.repository
{
    public interface IUserRepository
    {
        public User Create(User user);
        public User? GetById(int id);
        public List<User> GetAll();
        public User Update(User user);
        public bool Delete(int id);
    }
}
