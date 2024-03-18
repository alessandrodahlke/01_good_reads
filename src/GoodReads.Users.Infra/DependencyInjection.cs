using GoodReadas.Users.Domain.Repositories;
using GoodReads.Core.MessageBus;
using GoodReads.Users.Infra.Persistence;
using GoodReads.Users.Infra.Persistence.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodReads.Users.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration)
                .AddRabbitMq(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersContext>(options =>
                           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();

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
