using AlpataTech.MeetingAppDemo.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class Meeting : BaseEntitiy
    {
        public String Title { get; set; }
        public String Description { get; set;}
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int OrganizerId { get; set; }
        public User Organizer { get; set; }
        public List<MeetingParticipant> Participants { get; set; }
        public List<MeetingDocument> Documents { get; set; }

    }
}
