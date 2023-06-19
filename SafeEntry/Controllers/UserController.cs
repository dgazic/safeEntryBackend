using Microsoft.AspNetCore.Mvc;
using SafeEntry.Core.Interfaces;

namespace SafeEntry.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController :  ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(int userRoleId)
        {
            try
            {
                var users = await _service.GetUsers(userRoleId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
