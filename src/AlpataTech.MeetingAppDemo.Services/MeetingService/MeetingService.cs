using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AutoMapper;

namespace AlpataTech.MeetingAppDemo.Services.MeetingService
{
    public class MeetingService : IMeetingService
    {
        private readonly MeetingRepository _meetingRepository;
        private readonly IMapper _mapper;

        public MeetingService(MeetingRepository meetingRepository, IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }

        public MeetingDto CreateMeeting(CreateMeetingDto createMeetingDto)
        {
            var meeting = _mapper.Map<Meeting>(createMeetingDto);
            _meetingRepository.Add(meeting);
            _meetingRepository.SaveChanges();

            return _mapper.Map<MeetingDto>(createMeetingDto);
        }

        public void DeleteMeeting(int id)
        {
            var meeting = _meetingRepository.GetById(id);
            _meetingRepository.Remove(meeting);
            _meetingRepository.SaveChanges();
        }

        public IEnumerable<MeetingDto> GetAll()
        {
            var meetings = _meetingRepository.GetAll();
            return _mapper.Map<IEnumerable<MeetingDto>>(meetings);
        }

        public MeetingDto GetById(int id)
        {
            var meeting = _meetingRepository.GetById(id);
            return _mapper.Map<MeetingDto>(meeting);
        }

        public MeetingDto UpdateMeeting(UpdateMeetingDto updateMeetingDto)
        {
            throw new NotImplementedException();
        }
    }
}
