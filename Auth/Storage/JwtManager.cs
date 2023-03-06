using Auth.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Storage
{
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _configuration;
        Dictionary<string, string> UserRecords = new Dictionary<string, string>() { { "user1", "password1"}, { "user2", "password2" }, { "user3", "password3" } };

        public JwtManager(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public Tokens Authenticate(User user)
        {
            if (UserRecords.Any(u => u.Key == user.Name && u.Value == user.Password))
                return null!;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Name)
                    }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
