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

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with id : {id} not found");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var userDto = await _userService.CreateUserAsync(createUserDto);
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);

            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

    }
}