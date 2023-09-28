using AlpataTech.MeetingAppDemo.Entities.Common;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class MeetingParticipant : BaseEntity
    {
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
