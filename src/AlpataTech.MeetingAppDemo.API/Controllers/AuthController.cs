using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace AlpataTech.MeetingAppDemo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> AuthRegister()
        {
            // Implement
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> AuthLogin(UserAuthDto userAuthDto)
        {
            try
            {
                // Authenticate user and generate a JWT token
                var jwtToken = await _authService.GenerateJWT(userAuthDto);

                // If authentication is successful, return the JWT token
                return Ok(new { Token = jwtToken });
            }
            catch (Exception ex)
            {
                // Handle any authentication errors
                return BadRequest(new { message = "Authentication failed", error = ex.Message });
            }
        }
    }
}
