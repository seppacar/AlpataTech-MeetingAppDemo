using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities.DTO.User
{
    public class CreateUserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        [MinLength(8)] // Add password validation rules as needed
        public string Password { get; set; }
    }
}
