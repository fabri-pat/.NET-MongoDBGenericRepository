using System;
using MongoDbBaseRepository;

namespace test
{
    public class User : IEntity<Guid>
    {
        public User(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; init; }
    }
}