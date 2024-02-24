using MongoDB.Driver;

namespace UserServiceApi.Extensions
{
    public static class MongoDbExtension
    {
        public static IServiceCollection  AddMongoDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetConnectionString("MongoDb"));
            var mongoDatabase = mongoClient.GetDatabase(configuration["DatabaseName"]);
            services.AddScoped(provider => mongoDatabase);
            return services;
        }
    }
}
