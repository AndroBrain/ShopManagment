using Microsoft.IdentityModel.Tokens;
using ShopManagmentAPI.domain.model.user;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ShopManagmentAPI.domain.service.user;

public interface IUserService
{
    public string generateJWT(User user);
}
