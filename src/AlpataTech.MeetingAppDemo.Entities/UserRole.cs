using AlpataTech.MeetingAppDemo.Entities.Common;

namespace AlpataTech.MeetingAppDemo.Entities
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
