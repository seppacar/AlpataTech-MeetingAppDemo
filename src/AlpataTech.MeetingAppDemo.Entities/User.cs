using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class User : BaseEntitiy
    {
        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public String? PhoneNumber { get; set; }

        [Required]
        public String Password { get; set; } // TODO: Store password hashed, hash and salt perhaps?

        [Required]
        public String ProfileImage { get; set; } // Path of the profile image
    }
}