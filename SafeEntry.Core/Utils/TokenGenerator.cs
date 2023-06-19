using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SafeEntry.Persistance.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SafeEntry.Core.Utils
{
    public static class TokenGenerator
    {
        public static string CreateToken(UserModel user, IConfiguration configuration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("LastNameFirstName" , user.LastName + ' ' + user.FirstName),
                new Claim("UserRoleId", user.UserRoleId.ToString()),
                new Claim("UserId", user.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:SecretKey").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            configuration["AppSettings:Token"] = jwt;
            return jwt;
        }
    }
}
