using SafeEntry.Persistance.Models;

namespace SafeEntry.Persistance.Interfaces
{
    public interface IUserPersistance : IAsyncPersistance<UserModel>
    {
        public Task<UserModel> GetUserByEmail(string email);
        public Task<UserModel> UpdateUserActivationToken(UserModel model);

        public Task<UserModel> GetUserByActivationToken(string activationToken);
        public Task<IEnumerable<UserModel>> GetUsers(int userRoleId);
    }
}
