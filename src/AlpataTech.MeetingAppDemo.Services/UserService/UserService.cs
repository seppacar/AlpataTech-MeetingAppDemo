using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AutoMapper;
using System.Linq.Expressions;

namespace AlpataTech.MeetingAppDemo.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            // Map createUserDto type to User type
            var user = _mapper.Map<User>(createUserDto);

            // Hash the password using BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
            user.PasswordHash = passwordHash;

            // TODO: PROFILE IMAGE

            // Add the user asynchronously
            await _userRepository.AddAsync(user);

            // Save changes to the database
            await _userRepository.SaveChangesAsync();

            // Map the created user to UserDto and return
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> FindUsersAsync(Expression<Func<User, bool>> predicate)
        {
            var users = await _userRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserAsync(int id)
        {
            _userRepository.Remove(await _userRepository.GetByIdAsync(id));
            await _userRepository.SaveChangesAsync();
        }
    }
}
