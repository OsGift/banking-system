using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BankingSystem.Infrastructure.Services
{
    public class AuthenticationService
    {
        private readonly string _jwtSecret;

        public AuthenticationService(string jwtSecret)
        {
            _jwtSecret = jwtSecret;
        }

        public string GenerateToken(string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role) // Assign role to the token
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "BankingSystem",
                audience: "BankingSystem",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return claimsPrincipal;
            }
            catch
            {
                throw new SecurityTokenException("Invalid token");
            }
        }
    }
}
