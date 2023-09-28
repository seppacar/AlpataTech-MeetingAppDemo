using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingParticipant;
using AutoMapper;

namespace AlpataTech.MeetingAppDemo.Services.Common.Mapper
{
    public class MeetingParticipantProfile : Profile
    {
        public MeetingParticipantProfile()
        {
            CreateMap<MeetingParticipant, MeetingParticipantDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));
            CreateMap<MeetingParticipantDto, MeetingParticipant>();
            CreateMap<CreateMeetingParticipantDto, MeetingParticipant>();
        }
    }
}
