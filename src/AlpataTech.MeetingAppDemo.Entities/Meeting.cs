using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class Meeting : BaseEntitiy
    {
        [Required]
        public String Title { get; set; }

        [Required]
        public String Description { get; set;}

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        // Navigation property to Organizer
        [Required]
        public User Organizer { get; set; }

        // Navigation property to Participants
        public List<User> Participants { get; set; }
    }
}
