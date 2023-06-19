using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;
using SafeEntry.Core.Utils;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace SafeEntry.Core.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserPersistance _userPersistance;
        private readonly IConfiguration _configuration;


        public UserLoginService(IUserPersistance userPersistance, IConfiguration configuration)
        {
            _userPersistance = userPersistance;
            _configuration = configuration; 
        }

        public async Task<UserLoginResponse> UserLogin(UserLoginDto request)
        {
            var response = await VerifyLogin(request);
            return response;
        }

        private async Task<UserLoginResponse> VerifyLogin(UserLoginDto userLogin)
        {
            var response = new UserLoginResponse();
            var user = await _userPersistance.GetUserByEmail(userLogin.Email);

            if (user != null)
            {
                var sha256 = SHA256.Create();

                byte[] password = (sha256.ComputeHash(Encoding.UTF8.GetBytes((userLogin.Password) + user.SaltPassword)));
                bool areEqual = StructuralComparisons.StructuralEqualityComparer.Equals(password, user.Password);
                if (areEqual) {
                    response.Success = true;
                    response.Token = TokenGenerator.CreateToken(user, _configuration);
                    return response;
                }
                
                return response;
                    
            }
            else
                return response;
        }

    }
}
