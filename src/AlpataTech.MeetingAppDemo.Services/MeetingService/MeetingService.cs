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
            await _meetingRepository.AddAsync(meeting);

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

        public async Task<MeetingDto> UpdateMeetingAsync(int id, UpdateMeetingDto updateMeetingDto)
        {
            var meetingToUpdate = await _meetingRepository.GetByIdAsync(id);

            if (meetingToUpdate == null)
            {
                return null;
            }

            // Update the user properties with the values from updateUserDto
            _mapper.Map(updateMeetingDto, meetingToUpdate);

            // Update the user in the repository
            _meetingRepository.Update(meetingToUpdate);
            await _meetingRepository.SaveChangesAsync();

            // Map and return the updated user
            return _mapper.Map<MeetingDto>(meetingToUpdate);
        }

        public async Task DeleteMeetingAsync(int id)
        {
            _meetingRepository.Remove(await _meetingRepository.GetByIdAsync(id));
            await _meetingRepository.SaveChangesAsync();
        }
    }
}
