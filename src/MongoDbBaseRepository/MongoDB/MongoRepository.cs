
using System.Linq.Expressions;
using MongoDB.Driver;

namespace MongoDbBaseRepository.MongoDB
{
    public class MongoRepository<T, K> : IRepository<T, K> where T : IEntity<K>
    {
        protected IMongoCollection<T> dbCollection;
        protected FilterDefinitionBuilder<T> filterDefinitionBuilder = Builders<T>.Filter;
        protected UpdateDefinitionBuilder<T> updateDefinitionBuilder = Builders<T>.Update;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(filterDefinitionBuilder.Empty).ToListAsync();
        }
        
        public async Task<IReadOnlyCollection<T>> GeAllByExpressionAsync(Expression<Func<T, bool>> filter)
        {
            return await dbCollection.Find(filter).ToListAsync();
        }

        public async Task<T> GetByIdAsync(K id)
        {
            FilterDefinition<T> filter = filterDefinitionBuilder.Eq(
                entity => entity.Id, id
            );

            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> filter)
        {
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await dbCollection.InsertOneAsync(entity);
            return;
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            FilterDefinition<T> filter = filterDefinitionBuilder.Eq(x => x.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(K id)
        {
            FilterDefinition<T> filter = filterDefinitionBuilder.Eq(x => x.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}