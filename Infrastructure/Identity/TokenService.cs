using Application.Services.TokenService;
using Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateToken(User user)
        {
            List<Claim> authClaims = new()
            {
                new Claim (ClaimTypes.GivenName,user.UserName),
                new Claim (ClaimTypes.Email,user.Email),
                new Claim (ClaimTypes.NameIdentifier,user.Id),
            };

            SymmetricSecurityKey authKey = new(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            JwtSecurityToken token = new(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
