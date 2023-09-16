using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace AlpataTech.MeetingAppDemo.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        /* Admin Authorized Routes */

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto createUserDto)
        {   
            _userService.CreateUser(createUserDto);
            return Ok("Created");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            _userService.UpdateUser(updateUserDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }

        /* Public Routes  */

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] CreateUserDto createUserDto)
        {
            _userService.RegisterUser(createUserDto);
            return Ok();
        }

        /* User Routes (me) */

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            return Ok();
        }

        [HttpPut("me")]
        public IActionResult UpdateCurrentUser()
        {
            return Ok();
        }

        [HttpPost("me/change-password")]
        public IActionResult ChangePassword()
        {
            // Implementation for changing the user's password goes here
            return Ok(); // Placeholder response
        }
    }
}