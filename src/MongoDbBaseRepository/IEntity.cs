
namespace MongoDbBaseRepository
{
    public interface IEntity<K>
    {
        K Id { get; init; }
    }
}
