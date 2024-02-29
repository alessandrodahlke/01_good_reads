using GoodReads.Reviews.Domain.Repositories;
using GoodReads.Reviews.Infra.Persistence;
using GoodReads.Reviews.Infra.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra
{
    public static class DepndencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongo(configuration)
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();


                var options = new ReviewStoreDatabaseSettings
                {
                    ConnectionString = configuration.GetSection("MongoDb:ConnectionString").Value ?? throw new ArgumentNullException("MongoDb:ConnectionString"),
                    Database = configuration.GetSection("MongoDb:Database").Value ?? throw new ArgumentNullException("MongoDb:Database")
                };


                return options;
            });

            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetRequiredService<ReviewStoreDatabaseSettings>();

                return new MongoClient(options.ConnectionString);
            });

            services.AddTransient(sp =>
            {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

                var options = sp.GetRequiredService<ReviewStoreDatabaseSettings>();
                var client = sp.GetRequiredService<IMongoClient>();

                return client.GetDatabase(options.Database);
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IReadingRepository, ReadingRepository>();

            return services;
        }
    }
}
