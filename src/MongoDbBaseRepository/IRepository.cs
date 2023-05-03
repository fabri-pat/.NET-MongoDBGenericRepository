using System.Linq.Expressions;

namespace MongoDbBaseRepository
{
    public interface IRepository<T,K> where T : IEntity<K>
    {
        Task<T> GetByIdAsync(K id);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GeAllByExpressionAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(K id);
    }
}