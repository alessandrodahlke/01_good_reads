using GoodReads.Core.Data;
using GoodReads.Core.MessageBus;
using GoodReads.Reviews.Domain.Repositories;
using GoodReads.Reviews.Infra.Persistence;
using GoodReads.Reviews.Infra.Persistence.Repositories;
using GoodReads.Reviews.Infra.Rabbitmq;
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

                var options = new ReviewStoreDatabaseSettings
                {
                    ConnectionString = configuration.GetSection("MongoDb:ConnectionString").Value ?? throw new ArgumentNullException("MongoDb:ConnectionString"),
                    Database = configuration.GetSection("MongoDb:Database").Value ?? throw new ArgumentNullException("MongoDb:Database")
                };


                return options;
            });

            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReadingRepository, ReadingRepository>();

            return services;
        }

        private static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                var options = new RabbitMqSettings
                {
                    Host = configuration.GetSection("RabbitMq:Host").Value ?? throw new ArgumentNullException("RabbitMq:Host"),
                    Username = configuration.GetSection("RabbitMq:Username").Value ?? throw new ArgumentNullException("RabbitMq:Username"),
                    Password = configuration.GetSection("RabbitMq:Password").Value ?? throw new ArgumentNullException("RabbitMq:Password"),
                };

                return options;
            });

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumer<BookCreatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IMessageBus, MessageBus>();

            return services;
        }
    }
}
