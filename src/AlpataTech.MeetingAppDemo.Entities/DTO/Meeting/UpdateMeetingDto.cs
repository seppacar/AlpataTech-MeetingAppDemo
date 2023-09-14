namespace AlpataTech.MeetingAppDemo.Entities.DTO.Meeting
{
    public class UpdateMeetingDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int OrganizerId { get; set; }
    }
}
