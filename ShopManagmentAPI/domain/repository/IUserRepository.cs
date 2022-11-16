using ShopManagmentAPI.domain.model.user;

namespace ShopManagmentAPI.domain.repository
{
    public interface IUserRepository
    {
        public void Create(User user);
        public IdUser? GetByEmail(string email);
        public List<User> GetAll();
        public bool Update(string actualEmail, User user);
        public bool Delete(string email);
    }
}
