using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Services.UserService;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
        public async Task<IActionResult> CreateUser([FromForm] CreateUserDto createUserDto, IFormFile? profilePhoto)
        {
            var profilePictureFile = new FileUploadModel();

            // Check if a profile photo was provided
            if (profilePhoto != null)
            {
                // Check the file extension
                var fileExtension = Path.GetExtension(profilePhoto.FileName).ToLower();
                string[] permittedExtensions = { ".jpg", ".jpeg", "png", "webp" };

                // Check if the extension is permitted
                if (!permittedExtensions.Contains(fileExtension))
                {
                    return BadRequest("Unsupported file extension. Permitted extensions: .jpg, .jpeg, .png, .webp");
                }
                // Check if the content type is valid (image)
                if (!profilePhoto.ContentType.StartsWith("image/"))
                {
                    return BadRequest("Invalid content type. Only image files are allowed.");
                }

                // Set file extension
                profilePictureFile.FileExtension = fileExtension;

                // Convert IFormFile to byte array for the profile picture
                using (var ms = new MemoryStream())
                {
                    await profilePhoto.CopyToAsync(ms);
                    profilePictureFile.FileData = ms.ToArray();
                }
            }

            // Call service to create the user
            var userDto = await _userService.CreateUserAsync(createUserDto, profilePictureFile);
            // Return the created user
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        // Retrieving user profilepicture here but maybe we could TODO: sent as base 64 with all details
        [HttpGet("{id}/profilePicture")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserProfilePicture(int id)
        {
            byte[]? profilePictureBytes = await _userService.GetProfilePicture(id);
            return File(profilePictureBytes, "image/jpeg"); // Adjust the content type if needed
        }

        [HttpGet("roles/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoles(int id)
        {
            var roles = await _userService.GetUserRolesByIdAsync(id);
            return Ok(roles);
        }

        // TODO: Change password
        // TODO: Update Profile
        // TODO: Change Profile Picture
    }
}