using System.Linq.Expressions;

namespace MongoDbBaseRepository
{
    /// <summary>
    /// Repository interface.
    /// Define an interface that expose asynchronous CRUD methods.
    /// General purpose is to provide generic CRUD methods, using provided domain type and the domain type's id type.
    /// </summary>
    /// <typeparam name="T">The domain type the repository manages</typeparam>
    /// <typeparam name="K">The type of the id of the entity the repository manage</typeparam>
    public interface IRepository<T, K> where T : IEntity<K>
    {
        /// <summary>
        /// Asynchronous method that get the instance matching the specified id.
        /// </summary>
        /// <param name="id">The id of the instance you want to get.</param>
        /// <returns>The task with the instance found or null if no instance is saved.</returns>
        Task<T> GetByIdAsync(K id);

        /// <summary>
        /// Asynchronous method that get all instances saved.
        /// </summary>
        /// <returns>Task with an only readeable collection with all instances.</returns>
        Task<IReadOnlyCollection<T>> GetAllAsync();

        /// <summary>
        /// Asynchronous method that get a list of instance matching the specified filter.
        /// </summary>
        /// <param name="filter">Lambda expression that indicates specified selection filter</param>
        /// <returns>Task with an only readeable collection with all instances founded.</returns>
        Task<IReadOnlyCollection<T>> GeAllByExpressionAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Asynchronous method that get the instance matching the specified filter.
        /// </summary>
        /// <param name="filter">Lambda expression that indicates specified selection filter</param>
        /// <returns>Task with the instance found or null if no instance is saved.</returns>
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Asynchronous method that save the specified entity.
        /// </summary>
        /// <param name="entity">The entity to persist.</param>
        /// <returns>The task</returns>
        Task CreateAsync(T entity);

        /// <summary>
        /// Asynchronous method that update the specified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The task</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Asynchronous method that remove the specified entity.
        /// </summary>
        /// <param name="id">The id of the entity to remove.</param>
        /// <returns>The task</returns>
        Task RemoveAsync(K id);
    }
}