using GoodReads.Books.Application.Queries;
using GoodReads.Core.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoodReads.Books.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IBookQueries, BookQueries>();

            return services;
        }
    }
}
