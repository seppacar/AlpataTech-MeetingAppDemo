﻿using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingDocument;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingParticipant;
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
        Task<MeetingDto> AddMeetingParticipantAsync(int meetingId, CreateMeetingParticipantDto createMeetingParticipantDto);
        Task RemoveMeetingParticipantAsync(int meetingId, int participantUserId);
        Task<MeetingDocumentDto> AddMeetingDocumentAsync(int meetingId, FileUploadModel meetingDocumentUploadObject);
        Task<MeetingDocumentDto> GetMeetingDocumentObjectAsync(int meetingId, int meetingDocumentId);
        Task<byte[]> GetMeetingDocumentFileAsync(int meetingId, int meetingDocumentId);
        Task RemoveMeetingDocumentAsync(int meetingId, int meetingDocumentId);
        Task<bool> IsUserParticipant(int meetingId, int userId);
        Task<bool> IsUserOrganizer(int meetingId, int userId);
    }
}
