using System.IdentityModel.Tokens.Jwt;
using Users.Models;

namespace Users.Core
{
    public interface ITokenFactory
    {
        Task<JwtSecurityToken> GenerateTokenAsync(User user);
    }
}
