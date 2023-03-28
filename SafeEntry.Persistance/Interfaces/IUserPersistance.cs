using SafeEntry.Persistance.Models;

namespace SafeEntry.Persistance.Interfaces
{
    public interface IUserPersistance : IAsyncPersistance<UserModel>
    {
        public Task<UserModel> GetUserByEmail(string email);
    }
}
