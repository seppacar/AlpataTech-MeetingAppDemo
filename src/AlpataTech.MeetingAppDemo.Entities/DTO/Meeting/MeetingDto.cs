using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingDocument;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingParticipant;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;

namespace AlpataTech.MeetingAppDemo.Entities.DTO.Meeting
{
    public class MeetingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int OrganizerId { get; set; }
        public UserDto Organizer { get; set; }
        public List<MeetingParticipantDto> Participants { get; set; }
        public List<MeetingDocumentDto> Documents { get; set; }
    }
}
