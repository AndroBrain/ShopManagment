using ShopManagmentAPI.domain.model.user;

namespace ShopManagmentAPI.domain.model.authentication
{
    public class RegisterUserDto
    {
        public RegisterDto RegisterDto { get; set; }
        public UserRole UserRole { get; set; }
    }
}
