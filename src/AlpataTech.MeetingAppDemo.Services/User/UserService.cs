using AlpataTech.MeetingAppDemo.DAL.Repository;

namespace AlpataTech.MeetingAppDemo.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Get()
        {
            return("Testing repository ooo");
        }
    }
}
