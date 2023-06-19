using AutoMapper;
using Microsoft.Extensions.Logging;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserPersistance _userPersistance;
        public UserService(IUserPersistance userPersistance, IMapper mapper)
        {
            _userPersistance = userPersistance;
            _mapper = mapper;
        }
        public async Task<UserResponse> GetUsers(int userRoleId)
        {
            var response = new UserResponse();
            var users=  await _userPersistance.GetUsers(userRoleId);
            response.UserModelDto = _mapper.Map<IEnumerable<UserModel>, List<UserModelDto>>(users);
            return response;
        }
    }
}
