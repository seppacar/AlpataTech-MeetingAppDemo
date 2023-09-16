using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingParticipant;
using AutoMapper;

namespace AlpataTech.MeetingAppDemo.Services.Mapper
{
    public class MeetingParticipantProfile : Profile
    {
        public MeetingParticipantProfile() 
        {
            CreateMap<MeetingParticipant, MeetingParticipantDto>();
            CreateMap<MeetingParticipantDto, MeetingParticipantDto>();
        }
    }
}
