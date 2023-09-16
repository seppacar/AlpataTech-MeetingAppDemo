using AlpataTech.MeetingAppDemo.Entities.Common;
using System.Linq.Expressions;

namespace AlpataTech.MeetingAppDemo.Services.Common
{
    public interface IGenericService<T> where T : BaseEntity
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
        void Remove(Guid id);

        // Remove an entity from the repository.
        void Remove(T entity);
    }
}
