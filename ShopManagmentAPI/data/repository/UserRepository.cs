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
        public User Create(User user)
        {
            return UserMapper.EntityToModel(userDao.Create(UserMapper.ModelToEntity(user)));
        }

        public User? GetByEmail(string email)
        {
            var user = userDao.Get(email);
            if (user == null)
            {
                return null;
            } else
            {
                return UserMapper.EntityToModel(user);
            }
        }

        public List<User> GetAll()
        {
            return userDao.GetAll().Select(u => UserMapper.EntityToModel(u)).ToList();
        }

        public User Update(User user)
        {
            return UserMapper.EntityToModel(userDao.Update(UserMapper.ModelToEntity(user)));
        }

        public bool Delete(string email)
        {
            return userDao.Delete(email);
        }
    }
}
