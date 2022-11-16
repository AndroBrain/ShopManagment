using ShopManagmentAPI.data.db.user;
using ShopManagmentAPI.data.mappers;
using ShopManagmentAPI.domain.model.user;
using ShopManagmentAPI.domain.repository;

namespace ShopManagmentAPI.data.repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDao userDao;

        public UserRepository(IUserDao userDao) {
            this.userDao = userDao;
        }
        public void Create(User user)
        {
            var userEntity = userDao.Create(UserMapper.ModelToEntity(user));
            if (userEntity == null) throw new ArgumentException("User already exists");
        }

        public IdUser? GetByEmail(string email)
        {
            var user = userDao.Get(email);
            if (user == null)
            {
                return null;
            } else
            {
            return new IdUser() {
                    Id = user.Id,
                    User = UserMapper.EntityToModel(user)
                };
            }
        }

        public List<User> GetAll()
        {
            return userDao.GetAll().Select(u => UserMapper.EntityToModel(u)).ToList();
        }

        public bool Update(string actualEmail, User user)
        {
            return userDao.Update(actualEmail, UserMapper.ModelToEntity(user));
        }

        public bool Delete(string email)
        {
            return userDao.Delete(email);
        }
    }
}
