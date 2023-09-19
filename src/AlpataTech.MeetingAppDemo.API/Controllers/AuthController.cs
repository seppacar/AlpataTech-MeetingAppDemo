using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlpataTech.MeetingAppDemo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> AuthRegister()
        {
            string test = "test";
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> AuthLogin(UserAuthDto userAuthDto)
        {

            string test = "test";
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> AuthLogout()
        {
            string test = "test";
            return Ok();
        }
    }
}
