
namespace UserManagement.Providers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using UserManagement.Models;

    public class ApplicationJwtProvider
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        public ApplicationJwtProvider(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<string> JwtTokenBuilder(ApplicationUser user)
        {
            IList<string> roles = await userManager.GetRolesAsync(user);
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            Claim[] claims = {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Role, roles[0]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
          
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: configuration["Jwt:Iss"],
                audience: configuration["Jwt:Aud"],
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
