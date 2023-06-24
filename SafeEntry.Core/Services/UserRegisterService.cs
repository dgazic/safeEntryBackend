using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;
using SafeEntry.Core.Services.EmailService;
using SafeEntry.Core.Utils;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Core.Services
{
    public class UserRegisterService : IUserRegisterService
    {

        private readonly IUserPersistance _userPersistance;
        private readonly IEmailSender _emailsender;

        public UserRegisterService(IUserPersistance userPersistance, IEmailSender emailSender)
        {
            _userPersistance = userPersistance;
            _emailsender = emailSender;
        }
        public async Task<UserRegisterResponse> UserRegister(UserRegisterDto request)
        {
            var response = await Validate(request);
            if (response.Success)
            {
                string password = PasswordManager.GenerateOneTimePassword();
                byte[] salt = PasswordManager.GenerateSaltHash();
                byte[] passwordHash = PasswordManager.GeneratePasswordHash(password, salt);
                var userModel = new UserModel
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserRoleId = request.UserRoleId,
                    CompanyName = request.CompanyName,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    Password = passwordHash,
                    SaltPassword = salt
                };

                await _userPersistance.Insert(userModel);
                string subject, htmlContent;
                subject = "Safe Entry - Aktivacija računa";
                htmlContent = $"<strong>U poruci je jednokratna lozinka koju možete promjeniti nakon što se ulogirate<br>Lozinka: {password}</strong>";
                _emailsender.SendEmail(userModel, subject, htmlContent,null);
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
