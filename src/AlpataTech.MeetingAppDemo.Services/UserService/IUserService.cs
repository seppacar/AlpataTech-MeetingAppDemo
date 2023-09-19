using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using System.Linq.Expressions;

namespace AlpataTech.MeetingAppDemo.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto, byte[] profilePictureBytes);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> FindUsersAsync(Expression<Func<User, bool>> predicate);
        Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task DeleteUserAsync(int id);
        Task<byte[]?> GetProfilePicture(int id);
    }
}
