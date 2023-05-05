
namespace MongoDbBaseRepository
{
    /// <summary>
    /// The interface implemented by the entity class wanted to persist in the mongo collection.
    /// </summary>
    /// <typeparam name="K">The domain type's id type</typeparam>
    public interface IEntity<K>
    {
        K Id { get; init; }
    }
}
