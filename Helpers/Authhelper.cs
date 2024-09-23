using Microsoft.IdentityModel.Tokens;
using StudentTranscriptPortal.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentTranscriptPortal.Helpers
{
    public class Authhelper
    {
        private  readonly IConfiguration _configuration;

        public Authhelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),  // Subject (could be the username)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // Unique identifier
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            Console.WriteLine("Token generation process reached"); // Check if the code reaches this point
            Console.WriteLine(token);  // Output token to console
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

   

}
