using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserSecurityManagerProjectApi.Models;

namespace UserSecurityManagerProjectApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;

        public TokenService(JwtConfig configuration)
        {
            _jwtConfig = configuration;
        }

        public string GenerateSetPasswordToken(ApplicationUser user)
        {
            return WriteToken(user, 5, new Claim(ClaimTypes.NameIdentifier, user.Id), new Claim(ClaimTypes.Email, user.Email));
        }

        public string GenerateLoginToken(ApplicationUser user)
        {
            return WriteToken(user, 15, new Claim("Logged", "Logged"));
        }

        private string WriteToken(ApplicationUser user, int expiryMinutes, params Claim[] extraClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(expiryMinutes);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(extraClaims);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.JwtIssuer,
                audience: _jwtConfig.JwtAudience,
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
