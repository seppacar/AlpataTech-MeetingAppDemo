using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;

namespace AlpataTech.MeetingAppDemo.Services.UserService
{
    public interface IUserService
    {
        // CRUD
        public IEnumerable<UserDto> GetAll();

        public UserDto GetById(int id);

        public UserDto CreateUser(CreateUserDto createUserDto);

        public UserDto UpdateUser(UpdateUserDto updateUserDto);

        public void DeleteUser(int id);

        //

        public void RegisterUser(CreateUserDto createUserDto);

    }
}
