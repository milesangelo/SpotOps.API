using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using SpotOps.Api.Constants;
using SpotOps.Api.Models;
using SpotOps.Api.Settings;

namespace SpotOps.Api.Services.Interfaces
{
    public class JwtService : IJwtService
    {

        private string _secureKey = "todo, put some log string here to be the key";
        private readonly JwtOptions _jwt;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwt"></param>
        public JwtService(IOptions<JwtOptions> jwt)
        {
            _jwt = jwt.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userClaims"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public JwtSecurityToken Generate(ApplicationUser user, IList<Claim> userClaims, IList<string> userRoles)
        // public string Generate(ApplicationUser user, List<Claim> userClaims, List<string> roles)
        {
            //var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
            //var credentials = new SigningCredentials(symmetricSecurityKey,
            //    SecurityAlgorithms.HmacSha256Signature);
            //var header = new JwtHeader(credentials);

            //var payload = new JwtPayload(
            //    user.Id,
            //    null,
            //    null,
            //    null,
            //    DateTime.Today.AddDays(1)
            //);

            //var securityToken = new JwtSecurityToken(header, payload);

            //return new JwtSecurityTokenHandler().WriteToken(securityToken);

            //var userClaims = await _userManager.GetClaimsAsync(user);
            //    var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < userRoles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", userRoles[i]));
            }

            var claims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("uid", user.Id)
                    }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;

        }

        public string WriteToken(JwtSecurityToken jwt)
        {
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
