using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities.DTO.User
{
    public class UserAuthDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
