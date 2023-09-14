using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AutoMapper;

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

        public UserDto CreateUser(CreateUserDto createUserDto)
        {
            // TODO: Welcome email here
            var user = _mapper.Map<User>(createUserDto);
            _userRepository.Add(user);
            _userRepository.SaveChanges();
            return _mapper.Map<UserDto>(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Remove(id);
            _userRepository.SaveChanges(); // Save changes to the database
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public UserDto GetById(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public void RegisterUser(CreateUserDto createUserDto)
        {
            // TODO
            CreateUser(createUserDto);
        }

        public UserDto UpdateUser(UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
