using AlpataTech.MeetingAppDemo.Entities.Common;
using System.Linq.Expressions;

namespace AlpataTech.MeetingAppDemo.DAL.Repository.Common
{
    // The generic repository interface.
    // It's parameterized by a type T, which should be a subtype of BaseEntitiy.
    public interface IGenericRepository<T> where T : BaseEntitiy
    {
        // Retrieve an entity by its unique identifier.
        T GetById(int id);

        // Retrieve all entities of type T.
        IEnumerable<T> GetAll();

        // Retrieve entities that match a given condition.
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        // Add a new entity to the repository.
        void Add(T entity);

        // Add a collection of entities to the repository.
        void AddRange(IEnumerable<T> entities);

        // Update an existing entity in the repository.
        void Update(T entity);

        // Remove an entity from the repository by its unique identifier.
        void Remove(int id);

        // Remove an entity from the repository.
        void Remove(T entity);
    }
}
