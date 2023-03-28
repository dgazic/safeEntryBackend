using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Core.Services
{
    public class UserRegisterService : IUserRegisterService
    {

        private readonly IUserPersistance _userPersistance;

        public UserRegisterService(IUserPersistance userPersistance)
        {
            _userPersistance = userPersistance;
        }
        public async Task<UserRegisterResponse> UserRegister(UserRegisterDto request)
        {
            var response = await Validate(request);
            if (response.Success)
            {
                var userModel = new UserModel
                {
                    Email = request.Email,
                    UserName = request.Username,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };
                await _userPersistance.Insert(userModel);
            }
            return response;

        }

        private async Task<UserRegisterResponse> Validate(UserRegisterDto request)
        {
            var response = new UserRegisterResponse();
            bool userExist = false;
            var userEmail = await _userPersistance.GetUserByEmail(request.Email);
            if (userEmail != null)
                userExist = true;

            if (userExist == true)
            {
                response.Success = false;
                response.ValidationErrors.Add("Korisnik s unesenom email adresom već postoji!");
            }

            return response;
        }
    }
}
