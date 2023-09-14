using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AutoMapper;

namespace AlpataTech.MeetingAppDemo.Services.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, UserDto>(); // Map User to UserDto
            CreateMap<CreateUserDto, User>(); // Map CreateUserDto to User
            CreateMap<UpdateUserDto, User>(); // Map UpdateUserDto to User
        }
    }
}
