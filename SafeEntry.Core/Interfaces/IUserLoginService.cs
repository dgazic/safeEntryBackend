using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;

namespace SafeEntry.Core.Interfaces
{
    public interface IUserLoginService
    {
        Task<UserLoginResponse> UserLogin(UserLoginDto request);
    }
}
