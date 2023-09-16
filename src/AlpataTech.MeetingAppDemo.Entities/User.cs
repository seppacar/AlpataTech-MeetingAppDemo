using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class User : BaseEntitiy
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public string Email { get; set; }

        public String? PhoneNumber { get; set; }

        public String PasswordHash { get; set; } // TODO: Store password hashed, hash and salt perhaps?

        public String ProfileImage { get; set; } // Path of the profile image
    }
}