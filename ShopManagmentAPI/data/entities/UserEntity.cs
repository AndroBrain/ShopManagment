using ShopManagmentAPI.domain.model.user;

namespace ShopManagmentAPI.data.entities
{
    public class UserEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }
}
