using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AutoMapper;
using System.Linq.Expressions;

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
        public async Task<MeetingDto> CreateMeetingAsync(CreateMeetingDto createMeetingDto)
        {
            var meeting = _mapper.Map<Meeting>(createMeetingDto);

            return _mapper.Map<MeetingDto>(meeting);
        }

        public async Task<IEnumerable<MeetingDto>> GetAllMeetingsAsync()
        {
            var meetings = await _meetingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MeetingDto>>(meetings);
        }

        public async Task<MeetingDto> GetMeetingByIdAsync(int id)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);
            return _mapper.Map<MeetingDto>(meeting);
        }

        public async Task<IEnumerable<MeetingDto>> FindMeetingsAsync(Expression<Func<Meeting, bool>> predicate)
        {
            var meetings = await _meetingRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<MeetingDto>>(meetings);
        }

        public Task<MeetingDto> UpdateMeetingAsync(int id, UpdateMeetingDto updateMeetingDto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMeetingAsync(int id)
        {
            _meetingRepository.Remove(await _meetingRepository.GetByIdAsync(id));
            await _meetingRepository.SaveChangesAsync();
        }
    }
}
