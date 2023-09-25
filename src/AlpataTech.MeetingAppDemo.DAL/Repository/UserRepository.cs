using AlpataTech.MeetingAppDemo.DAL.Context;
using AlpataTech.MeetingAppDemo.DAL.Repository.Common;
using AlpataTech.MeetingAppDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlpataTech.MeetingAppDemo.DAL.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserWithNavigationsAsync(int id)
        {
            return await _dbContext.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
