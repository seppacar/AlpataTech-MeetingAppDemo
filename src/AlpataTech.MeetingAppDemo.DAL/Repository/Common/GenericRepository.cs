using AlpataTech.MeetingAppDemo.DAL.Context;
using AlpataTech.MeetingAppDemo.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AlpataTech.MeetingAppDemo.DAL.Repository.Common
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntitiy
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T>  _dbSet;
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Remove(Guid id)
        {
            T entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
