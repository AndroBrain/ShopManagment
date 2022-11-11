using ShopManagmentAPI.data.db;
using ShopManagmentAPI.data.mappers;
using ShopManagmentAPI.domain.model.user;
using ShopManagmentAPI.domain.repository;

namespace ShopManagmentAPI.data.repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDb userDb;

        public UserRepository(UserDb userDb) {
            this.userDb = userDb;
        }
        public User Create(User user)
        {
            return UserMapper.EntityToModel(userDb.Add(UserMapper.ModelToEntity(user)));
        }

        public User? GetByEmail(string email)
        {
            var user = userDb.Get(email);
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
            return userDb.GetAll().Select(u => UserMapper.EntityToModel(u)).ToList();
        }

        public User Update(User user)
        {
            return UserMapper.EntityToModel(userDb.Update(UserMapper.ModelToEntity(user)));
        }

        public bool Delete(string email)
        {
            return userDb.Remove(email);
        }
    }
}
