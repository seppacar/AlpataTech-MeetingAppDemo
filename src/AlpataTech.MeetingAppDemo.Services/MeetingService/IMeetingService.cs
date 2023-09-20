using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingParticipant;
using AlpataTech.MeetingAppDemo.Services.Common.Mapper;
using System.Linq.Expressions;

namespace AlpataTech.MeetingAppDemo.Services.MeetingService
{
    public interface IMeetingService
    {
        Task<MeetingDto> CreateMeetingAsync(CreateMeetingDto createMeetingDto);
        Task<IEnumerable<MeetingDto>> GetAllMeetingsAsync();
        Task<MeetingDto> GetMeetingByIdAsync(int id);
        Task<IEnumerable<MeetingDto>> FindMeetingsAsync(Expression<Func<Meeting, bool>> predicate);
        Task<MeetingDto> UpdateMeetingAsync(int id, UpdateMeetingDto updateMeetingDto);
        Task DeleteMeetingAsync(int id);
        Task<MeetingDto> AddMeetingParticipantAsync(int meetingId, MeetingParticipantDto meetingParticipantDto);
        Task<MeetingDto> AddMeetingDocumentAsync(int meetingId, FileUploadModel meetingDocument);
    }
}
