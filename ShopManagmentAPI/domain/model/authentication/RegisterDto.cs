﻿using ShopManagmentAPI.domain.model.user;

namespace ShopManagmentAPI.domain.model.authentication
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
