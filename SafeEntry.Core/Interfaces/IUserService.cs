using SafeEntry.Core.Responses;

namespace SafeEntry.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUsers(int userRoleId);
    }
}
