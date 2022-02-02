using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SpotOps.Api.Models;

namespace SpotOps.Api.Services.Interfaces;

public interface IJwtService
{
    public JwtSecurityToken Generate(ApplicationUser userId, IList<Claim> userClaims, IList<string> userRoles);

    public JwtSecurityToken Verify(string jwt);

    public string WriteToken(JwtSecurityToken jwt);
}