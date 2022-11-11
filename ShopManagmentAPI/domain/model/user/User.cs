using ShopManagmentAPI.data.entities;

namespace ShopManagmentAPI.domain.model.user
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }
}
