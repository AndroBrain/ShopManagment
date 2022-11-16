using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShopManagmentAPI.data.db;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain.model.authentication;
using ShopManagmentAPI.domain.model.user;
using ShopManagmentAPI.domain.repository;
using ShopManagmentAPI.domain.service.email;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopManagmentAPI.domain.service.user;

public class AuthenticationService : IAuthenticationService
{
    private readonly IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
    private readonly IUserRepository userRepository;
    private readonly IEmailSender emailSender;
    public AuthenticationService(IUserRepository userRepository, IEmailSender emailSender)
    {
        this.userRepository = userRepository;
        this.emailSender = emailSender;
    }

    public void RegisterUser(RegisterDto user, UserRole role)
    {
        var newUser = new User()
        {
            Email = user.Email,
            Name = user.Name,
            Role = role,
        };
        newUser.PasswordHash = passwordHasher.HashPassword(newUser, user.Password);
        userRepository.Create(newUser);
        emailSender.sendEmail(newUser.Email, "Hello", "Thx for registering for out Shop Managment System");
    }

    public IdUser? FindUserByEmail(string email)
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

    public IdUser? GetUserFromToken(HttpContext context)
    {
        var email = context.User.Identities.First()?.Claims?.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
        if (email == null)
            return null;
        return userRepository.GetByEmail(email);
    }

}
