using Microsoft.AspNetCore.Mvc;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;

namespace SafeEntry.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRegisterService _userRegisterService;
        private readonly IUserLoginService _userLoginService;

        public AuthController (IUserRegisterService userRegisterService, IUserLoginService userLoginService)
        {
            _userRegisterService = userRegisterService;
            _userLoginService = userLoginService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModelDto>> Register(UserRegisterDto request)
        {
            var response = await _userRegisterService.UserRegister(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            var response = await _userLoginService.UserLogin(request);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
