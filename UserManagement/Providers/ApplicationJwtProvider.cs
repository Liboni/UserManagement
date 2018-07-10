
namespace UserManagement.Providers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using UserManagement.Models;

    public class ApplicationJwtProvider
    {
        private readonly IConfiguration configuration;

        public ApplicationJwtProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string JwtTokenBuilder(ApplicationUser user)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            Claim[] claims = {
                                     new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                                     new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                                 };
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                configuration["Jwt:Iss"],
                configuration["Jwt:Aud"],
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
