using AlpataTech.MeetingAppDemo.DAL.Context;
using AlpataTech.MeetingAppDemo.DAL.Repository.Common;
using AlpataTech.MeetingAppDemo.Entities;

namespace AlpataTech.MeetingAppDemo.DAL.Repository
{
    public class UserRepository : GenericRepositoryNew<User>
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
