using AlpataTech.MeetingAppDemo.DAL.Context;
using AlpataTech.MeetingAppDemo.DAL.Repository.Common;
using AlpataTech.MeetingAppDemo.Entities;

namespace AlpataTech.MeetingAppDemo.DAL.Repository
{
    public class RoleRepository : GenericRepository<Role>
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task  AddUserRoleAsync(int userId, int roleId)
        {
            await _dbContext.UserRole.AddAsync(new UserRole
            {
                UserId = userId,
                RoleId = roleId
            });
        }
    }
}
