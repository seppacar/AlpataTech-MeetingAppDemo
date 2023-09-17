using AlpataTech.MeetingAppDemo.Entities.Common;
using System.Linq.Expressions;

namespace AlpataTech.MeetingAppDemo.DAL.Repository.Common
{
    public interface IGenericRepositoryNew<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChangesAsync(); // Async version
        void SaveChanges(); // Synchronous version
    }
}
