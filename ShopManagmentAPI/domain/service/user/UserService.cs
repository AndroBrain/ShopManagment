using Microsoft.IdentityModel.Tokens;
using ShopManagmentAPI.domain.model.user;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopManagmentAPI.domain.service.user;

public class UserService : IUserService
{
    public string generateJWT(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email)
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
