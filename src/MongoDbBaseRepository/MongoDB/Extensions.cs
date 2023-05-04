using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace MongoDbBaseRepository.MongoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var connectionString = configuration!.GetSection("MongoSettings:ConnectionString").Value;
                var databaseName = configuration.GetSection("MongoSettings:DatabaseName").Value;
                var settings = MongoClientSettings.FromConnectionString(connectionString);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                var mongoClient = new MongoClient(settings);

                return mongoClient.GetDatabase(databaseName);
            });

            return services;
        }

        public static IServiceCollection AddMongoRepository<T, K>(this IServiceCollection services, String collectionName)
            where T : IEntity<K>
        {
            services.AddSingleton<IRepository<T, K>, MongoRepository<T, K>>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<T, K>(database!, collectionName);
            });

            return services;
        }
    }
}