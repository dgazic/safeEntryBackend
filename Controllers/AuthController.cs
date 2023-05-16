using Microsoft.AspNetCore.Mvc;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRegisterService _userRegisterService;
        private readonly IUserLoginService _userLoginService;
        private readonly IActivationAccountService _activationAccountService;

        public AuthController (IUserRegisterService userRegisterService, IUserLoginService userLoginService, IActivationAccountService activationAccountService)
        {
            _userRegisterService = userRegisterService;
            _userLoginService = userLoginService;
            _activationAccountService = activationAccountService;
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

        [HttpPut("activateAccount")]
        public async Task<ActionResult<UserModelDto>> ActivationAccount(ActivationAccountDto request)
        {
            var response = await _activationAccountService.ActivationAccount(request);
            return Ok(response);
        }

        [HttpGet("IsActivationTokenValid")]
        public async Task<ActionResult<UserModelDto>> IsActivationTokenValid(string? activationToken)
        {
            var response = await _activationAccountService.IsActivationTokenValid(activationToken);

            if (!response.IsTokenValid)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
