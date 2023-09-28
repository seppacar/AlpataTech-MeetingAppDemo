using AlpataTech.MeetingAppDemo.DAL.Context;
using AlpataTech.MeetingAppDemo.DAL.Repository.Common;
using AlpataTech.MeetingAppDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlpataTech.MeetingAppDemo.DAL.Repository
{
    public class MeetingRepository : GenericRepository<Meeting>
    {
        public MeetingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Meeting> GetMeetingWithNavigationsAsync(int id)
        {
            return await _dbContext.Meetings
                .Include(m => m.Organizer)
                .Include(m => m.Participants)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
