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
        public int OrganizerId { get; set; }
        public User Organizer { get; set; }

        // Navigation property to MeetingParticipants
        public List<MeetingParticipant> Participants { get; set; }

        // Navigation property to MeetingDocuments
        public List<MeetingDocument> Documents { get; set; }

    }
}
