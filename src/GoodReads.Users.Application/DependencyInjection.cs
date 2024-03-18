using GoodReads.Core.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoodReads.Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            return services;
        }
    }
}
