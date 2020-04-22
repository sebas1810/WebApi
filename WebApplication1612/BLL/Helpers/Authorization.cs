using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Helpers
{
    public class Authorization
    {
        public static JwtSecurityToken GetToken(string secretKey, string name)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            claims.Add(new Claim(ClaimTypes.Role, "Everybody"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, name));
            claims.Add(new Claim("Custom_Claim", "CustomValue"));

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "Issuer",
               audience: "Audience",
               claims: claims,
               expires: DateTime.UtcNow.AddHours(2),
               signingCredentials: signingCredentials);
            return token;
        }
    }
}
