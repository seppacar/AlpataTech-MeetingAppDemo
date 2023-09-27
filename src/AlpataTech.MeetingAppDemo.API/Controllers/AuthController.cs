using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Services.AuthService;
using AlpataTech.MeetingAppDemo.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace AlpataTech.MeetingAppDemo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> AuthRegister([FromForm] CreateUserDto createUserDto, IFormFile profilePhoto)
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

            var userAuthDto = new UserAuthDto
            {
                Email = createUserDto.Email,
                Password = createUserDto.Password
            };

            // Generate JWT token
            var token = await _authService.GenerateJWT(userAuthDto);  // Implement this method

            // Return the JWT token along with the created user
            return Ok(new { user = userDto, token });
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
