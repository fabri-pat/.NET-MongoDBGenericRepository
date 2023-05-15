using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDbBaseRepository.MongoDB;
using Moq;
using Xunit;

namespace test
{
    public class MongoRepositoryTests
    {
        ServiceProvider serviceProvider;

        public MongoRepositoryTests(){
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("C:\\Users\\fabri\\Desktop\\MongoDbBaseRepository\\test\\appsettings.json")
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // Act
            services.AddMongo().AddMongoRepository<User, Guid>("Test");

            serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void shouldDatabaseInstanceBeNotNull()
        {
            Assert.NotNull(serviceProvider.GetService<IMongoDatabase>());
        }
    }
}

