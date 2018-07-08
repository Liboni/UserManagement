using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Providers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using UserManagement.Models;

    public class ApplicationJwtProvider
    {
        public static IConfiguration Configuration { get; set; }
        public string JwtTokenBuilder(ApplicationUser user)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            Claim[] claims = {
                                     new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                                     new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                                 };
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                Configuration["Jwt:Iss"],
                Configuration["Jwt:Aud"],
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
