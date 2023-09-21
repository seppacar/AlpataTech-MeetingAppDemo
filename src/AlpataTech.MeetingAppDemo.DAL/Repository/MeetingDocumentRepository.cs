using AlpataTech.MeetingAppDemo.DAL.Context;
using AlpataTech.MeetingAppDemo.DAL.Repository.Common;
using AlpataTech.MeetingAppDemo.Entities;

namespace AlpataTech.MeetingAppDemo.DAL.Repository
{
    public class MeetingDocumentRepository : GenericRepository<MeetingDocument>
    {
        public MeetingDocumentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
