using ShopManagmentAPI.data.db;
using ShopManagmentAPI.data.mappers;
using ShopManagmentAPI.domain.model;
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

        public User? GetById(int id)
        {
            var user = userDb.Get(id);
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

        public bool Delete(int id)
        {
            return userDb.Remove(id);
        }
    }
}
