using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class MeetingParticipant : BaseEntitiy
    {
        // Navigation property to the associated Meeting
        [Required]
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }

        // Navigation property to the associated User
        public int? UserId { get; set; }
        public User User { get; set; }

        // Attendee details (if they are not users)
        public string? AttendeeFirstName { get; set; }
        public string? AttendeeLastName { get; set; }
        public string? AttendeeEmail { get; set; }
    }
}
