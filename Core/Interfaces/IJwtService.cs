using Core.Entities;
using System.Security.Claims;

namespace Core.Interfaces
{
    public interface IJwtService
    {
        IEnumerable<Claim> GetClaims(User user);
        string CreateToken(IEnumerable<Claim> claims);
    }
}