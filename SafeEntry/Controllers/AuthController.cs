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
    

        public AuthController (IUserRegisterService userRegisterService, IUserLoginService userLoginService)
        {
            _userRegisterService = userRegisterService;
            _userLoginService = userLoginService;
            
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModelDto>> Register(UserRegisterDto request)
        {
            try
            {
                var response = await _userRegisterService.UserRegister(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            try
            {
                var response = await _userLoginService.UserLogin(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
