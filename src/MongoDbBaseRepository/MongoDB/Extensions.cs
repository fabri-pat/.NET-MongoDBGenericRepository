using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace MongoDbBaseRepository.MongoDB
{
    public static class Extensions
    {
        /// <summary>
        /// Method used in program.cs file to add MongoDB repository as singleton.
        /// In appsettings.json, it must be declared the connection string and the database name.     
        ///  
        /// <example>
        /// <code>   
        ///     "MongoSettings":{
        ///         "ConnectionString": CONNECTIONSTRING, 
        ///         "DatabaseName": DATABASENAME
        ///    }
        ///    
        /// </code>
        /// </example>
        /// </summary>
        /// <returns>Services with declared mongo repository as singleton.</returns>
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

        /// <summary>
        /// Method used in program.cs file to register generic interface to its implementation
        /// with specified domain types and the name of provided mongoDB collection.
        /// </summary>
        /// <typeparam name="T">The domain type the repository manages</typeparam>
        /// <typeparam name="K">The domain type's id type</typeparam>
        /// <param name="collectionName">The name of mongoDB collection</param>
        /// <returns>Services with declared mongo repository as singleton.</returns>
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