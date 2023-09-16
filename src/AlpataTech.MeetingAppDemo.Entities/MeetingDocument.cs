using AlpataTech.MeetingAppDemo.Entities.Common;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class MeetingDocument : BaseEntity
    {
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentPath { get; set; }
    }
}
