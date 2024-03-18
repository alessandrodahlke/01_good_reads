using GoodReads.Core.Mediator;
using GoodReads.Reviews.Application.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoodReads.Reviews.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IBookQueries, BookQueries>();
            services.AddScoped<IUserQueries, UserQueries>();

            return services;
        }
    }
}
