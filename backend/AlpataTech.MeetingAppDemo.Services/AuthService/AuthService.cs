using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Services.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlpataTech.MeetingAppDemo.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }
        public async Task<string> GenerateJWT(UserAuthDto userAuthDto)
        {
            var user = await _userService.GetUserByEmailAsync(userAuthDto.Email);

            // If user is not found or password cannot be verified throw an exception
            if (user == null || !BCrypt.Net.BCrypt.Verify(userAuthDto.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password.");
            }

            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Get User Roles and add to JWT Claims
            var roles = await _userService.GetUserRolesByIdAsync(user.Id);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JWT:Secret"]));

            var token = new JwtSecurityToken(
                _configuration["Authentication:JWT:Issuer"],
                _configuration["Authentication:JWT:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1), // Adjust token expiration as needed
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}
