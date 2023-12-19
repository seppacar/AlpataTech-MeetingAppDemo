using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public String FirstName { get; set; } = String.Empty;

        [Required]
        public String LastName { get; set; } = String.Empty;

        [Required]
        public String? Email { get; set; } = String.Empty;

        public String? PhoneNumber { get; set; }

        [Required]
        public String PasswordHash { get; set; } = String.Empty;

        [Required]
        public String ProfileImage { get; set; } = String.Empty;

        public List<UserRole> Roles { get; set; }
    }
}