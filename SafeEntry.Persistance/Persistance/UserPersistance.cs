using Dapper;
using SafeEntry.Persistance.Context;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;
using System.Data;

namespace SafeEntry.Persistance.Persistance
{
    public class UserPersistance : IUserPersistance
    {
        private readonly DapperContext _context;

        public Task<IEnumerable<UserModel>> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Email = email
                };
                var userModel = await connection.QueryAsync<UserModel>("SELECT * FROM get_users_in_db(@Email)", parameters);
                return userModel.FirstOrDefault();
            }
        }

        public async Task<UserModel> Insert(UserModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                  Username = model.UserName,
                  Email = model.Email,
                  LastName = model.LastName,
                  FirstName = model.FirstName
                  
                };

                var userModel = await connection.QueryAsync<UserModel>("CALL EventRegistration_Insert(@Name,@Description,@OrganizerId)", parameters);
                return userModel.FirstOrDefault();
            }
        }
    }
}
