using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IJwtService
    {
        IEnumerable<Claim> GetClaims(IdentityUser user);
        string CreateToken(IEnumerable<Claim> claims);
    }
}