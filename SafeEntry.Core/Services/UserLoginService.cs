using Microsoft.Extensions.Options;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;
using SafeEntry.Persistance.Interfaces;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace SafeEntry.Core.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserPersistance _userPersistance;


        public UserLoginService(IUserPersistance userPersistance)
        {
            _userPersistance = userPersistance;
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

                byte[] password = (sha256.ComputeHash(Encoding.UTF8.GetBytes(userLogin.Password)));
                bool areEqual = StructuralComparisons.StructuralEqualityComparer.Equals(password, user.Password);
                if (areEqual) {
                    response.Success = true;
                    return response;
                }
                return response;
                    
            }
            else
                return response;
        }

    }
}
