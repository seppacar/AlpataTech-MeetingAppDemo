using AlpataTech.MeetingAppDemo.DAL.Context;
using AlpataTech.MeetingAppDemo.DAL.Repository.Common;
using AlpataTech.MeetingAppDemo.Entities;

namespace AlpataTech.MeetingAppDemo.DAL.Repository
{
    public class MeetingRepository : GenericRepositoryNew<Meeting>
    {
        public MeetingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
