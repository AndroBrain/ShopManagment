using Microsoft.IdentityModel.Tokens;
using ShopManagmentAPI.domain.model.user;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ShopManagmentAPI.domain.service.user;

public interface IAuthenticationService
{
    public void RegisterUser(RegisterUserDTO user);
    public User? FindUserByEmail(string email);
    public bool VerifyPasswordHashes(User user, string loginPassword);
    public string GenerateJWT(User user);
}
