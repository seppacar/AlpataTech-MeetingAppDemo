using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AutoMapper;

namespace AlpataTech.MeetingAppDemo.Services.Common.Mapper
{
    public class MeetingProfile : Profile
    {
        public MeetingProfile()
        {
            CreateMap<Meeting, MeetingDto>(); // Map Meeting to MeetingDto
            CreateMap<CreateMeetingDto, Meeting>(); // Map CreateMeetingDto to Meeting
            CreateMap<UpdateMeetingDto, Meeting>(); // Map UpdateMeetingDto to Meeting
        }
    }
}
