using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;

namespace SafeEntry.Core.Interfaces
{
    public interface IUserRegisterService
    {
        Task<UserRegisterResponse> UserRegister(UserRegisterDto request);
    }
}
