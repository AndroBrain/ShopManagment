using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShopManagmentAPI.data.db;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain.model.user;
using ShopManagmentAPI.domain.repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopManagmentAPI.domain.service.user;

public class AuthenticationService : IAuthenticationService
{
    private readonly IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
    private readonly IUserRepository userRepository = new UserRepository(new UserDb());
    public AuthenticationService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void RegisterUser(RegisterUserDTO user)
    {
        var newUser = new User()
        {
            Email = user.Email,
            Name = user.Name,
            Role = user.Role,
        };
        newUser.PasswordHash = passwordHasher.HashPassword(newUser, user.Password);
        userRepository.Create(newUser);
    }

    public User? FindUserByEmail(string email)
    {
        return userRepository.GetByEmail(email);
    }

    public bool VerifyPasswordHashes(User user, string loginPassword)
    {
        return passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginPassword) == PasswordVerificationResult.Success;
    }
    public string GenerateJWT(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationSettings.Key));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(AuthenticationSettings.ExpireDays);

        var token = new JwtSecurityToken(AuthenticationSettings.Issuer,
            AuthenticationSettings.Issuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

}
