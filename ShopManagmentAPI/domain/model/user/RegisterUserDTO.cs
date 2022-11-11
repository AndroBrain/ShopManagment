namespace ShopManagmentAPI.domain.model.user
{
    public class RegisterUserDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
