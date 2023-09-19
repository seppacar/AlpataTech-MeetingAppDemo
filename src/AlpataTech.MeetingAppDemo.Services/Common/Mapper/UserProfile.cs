using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AutoMapper;

namespace AlpataTech.MeetingAppDemo.Services.Common.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(ur => ur.Role.Name)));
            CreateMap<CreateUserDto, User>(); // Map CreateUserDto to User
            CreateMap<UpdateUserDto, User>(); // Map UpdateUserDto to User
        }
    }
}
