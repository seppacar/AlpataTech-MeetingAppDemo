using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class Meeting : BaseEntitiy
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set;}

        [Required(ErrorMessage = "StartTime is required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required")]
        public DateTime EndTime { get; set; }

        public int OrganizerId { get; set; }
        public User Organizer { get; set; }
        public List<MeetingParticipant> Participants { get; set; }
        public List<MeetingDocument> Documents { get; set; }

    }
}
