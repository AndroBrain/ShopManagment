using ShopManagmentAPI.data.entities;

namespace ShopManagmentAPI.domain.model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }

    }
}
