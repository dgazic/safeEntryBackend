using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;
using SafeEntry.Core.Utils;
using SafeEntry.Core.Validators;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Core.Services
{
    public class ActivationAccountService : IActivationAccountService
    {
        private readonly IUserPersistance _userPersistance;
        public ActivationAccountService(IUserPersistance userPersistance) 
        {
            _userPersistance= userPersistance;
        }   

        public async Task<ActivationAccountResponse> ActivationAccount(ActivationAccountDto request)
        {
            var response = await Validate(request);

            if (response.Success)
            {
                byte[] salt = PasswordManager.GenerateSaltHash();
                byte[] passwordHash = PasswordManager.GeneratePasswordHash(request.Password, salt);
                var user = await _userPersistance.GetUserByActivationToken(request.ActivationToken);

                var userModel = new UserModel { Password = passwordHash, SaltPassword = salt, Id = user.Id };
                await _userPersistance.UpdateUserActivationToken(userModel);
            }
            return response;
        }
        private async Task<ActivationAccountResponse> Validate(ActivationAccountDto request)
        {
            var response = new ActivationAccountResponse();
            var validator = new ActivationAccountValidator(_userPersistance);
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Count > 0)
            {
                response.Success = false;
                foreach (var error in validatorResult.Errors)
                    response.ValidationErrors.Add(error.ErrorMessage);
            }
            return response;
        }

        public async Task<ActivationAccountResponse> IsActivationTokenValid(String activationToken)
        {
            var user = await _userPersistance.GetUserByActivationToken(activationToken);
            var response = new ActivationAccountResponse();
            if (user == null)
                response.IsTokenValid = false;
            return response;
        }
    }
}
