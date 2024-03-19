using GoodReads.Core.Data;
using GoodReads.Core.MessageBus;
using GoodReads.Reviews.Domain.Repositories;
using GoodReads.Reviews.Infra.Persistence;
using GoodReads.Reviews.Infra.Persistence.Repositories;
using GoodReads.Reviews.Infra.Rabbitmq.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodReads.Reviews.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongo()
                .AddRepositories()
                .AddRabbitMq(configuration);

            return services;
        }

        private static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                var options = new MongoConfig
                {
                    ConnectionString = configuration.GetSection("MongoDb:ConnectionString").Value ?? throw new ArgumentNullException("MongoDb:ConnectionString"),
                    Database = configuration.GetSection("MongoDb:Database").Value ?? throw new ArgumentNullException("MongoDb:Database")
                };


                return options;
            });

            services.AddScoped<IMongoContext, MongoContext>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IReadingRepository, ReadingRepository>();

            return services;
        }

        private static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration.GetSection("RabbitMq:Host").Value ?? throw new ArgumentNullException("RabbitMq:Host");
            var username = configuration.GetSection("RabbitMq:Username").Value ?? throw new ArgumentNullException("RabbitMq:Username");
            var password = configuration.GetSection("RabbitMq:Password").Value ?? throw new ArgumentNullException("RabbitMq:Password");

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumer<BookCreatedConsumer>();
                x.AddConsumer<UserCreatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(host), h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IMessageBus, MessageBus>();

            return services;
        }
    }
}
