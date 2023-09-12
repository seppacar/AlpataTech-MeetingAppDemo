using AlpataTech.MeetingAppDemo.DAL.Context;
using AlpataTech.MeetingAppDemo.DAL.Repository.Common;
using AlpataTech.MeetingAppDemo.Entities;

namespace AlpataTech.MeetingAppDemo.DAL.Repository
{
    internal class MeetingRepository : GenericRepository<Meeting>
    {
        public MeetingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
