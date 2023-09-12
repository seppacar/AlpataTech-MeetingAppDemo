using AlpataTech.MeetingAppDemo.Entities;

namespace AlpataTech.MeetingAppDemo.Services.UserService
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();
    }
}
