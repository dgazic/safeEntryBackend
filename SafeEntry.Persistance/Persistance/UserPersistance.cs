using Dapper;
using Microsoft.AspNetCore.WebUtilities;
using SafeEntry.Persistance.Context;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;
using System.Data;

namespace SafeEntry.Persistance.Persistance
{
    public class UserPersistance : IUserPersistance
    {
        private readonly DapperContext _context;

        public UserPersistance(DapperContext context)
        {
            _context = context;
        }


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
                  FirstName = model.FirstName,
                  UserRoleId = model.UserRoleId
                  
                };

                var userModel = await connection.QueryAsync<UserModel>("CALL User_Insert(@Username,@Email,@LastName,@FirstName,@UserRoleId)", parameters);
                return userModel.FirstOrDefault();
            }
        }

        public async Task<UserModel> UpdateUserActivationToken(UserModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Password = model.Password,
                    SaltPassword = model.SaltPassword,
                    Activated = model.Activated,
                    UserId = model.Id
                };

                var userModels = await connection.QueryAsync<UserModel>("dbo.PasswordReset_Update", parameters,
                commandType: CommandType.StoredProcedure);
                return userModels.FirstOrDefault();
            }
        }
        public async Task<UserModel> GetUserByActivationToken(string activationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                byte[] decodedToken = WebEncoders.Base64UrlDecode(activationToken);
                var parameters = new { ActivationToken = decodedToken };
                var userModels = await connection.QueryAsync<UserModel>("dbo.ActivationTokenGetUser_Select", parameters,
                commandType: CommandType.StoredProcedure);
                return userModels.FirstOrDefault();
            }
        }
    }
}
