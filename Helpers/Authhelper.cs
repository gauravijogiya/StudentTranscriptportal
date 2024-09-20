using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentTranscriptPortal.Helpers
{
    public class Authhelper
    {
        public static string GenerateToken(string key, string issuer, string audience, int expireMinutes = 30)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "TestUser"),  // Subject (could be the username)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // Unique identifier
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: credentials);
            Console.WriteLine("Token generation process reached"); // Check if the code reaches this point
            Console.WriteLine(token);  // Output token to console
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

   

}
