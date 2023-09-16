using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;

namespace AlpataTech.MeetingAppDemo.Services.MeetingService
{
    public interface IMeetingService
    {
        // CRUD
        public IEnumerable<MeetingDto> GetAll();

        public MeetingDto GetById(int id);

        public MeetingDto CreateMeeting(CreateMeetingDto createMeetingDto);

        public MeetingDto UpdateMeeting(UpdateMeetingDto updateMeetingDto);

        public void DeleteMeeting(int id);
    }
}
