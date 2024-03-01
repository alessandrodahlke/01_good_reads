using GoodReads.Core.Mediator;
using GoodReads.Core.Results;
using GoodReads.Reviews.Application.Commands;
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
            services.AddScoped<IRequestHandler<CreateReviewCommand, CustomResult>, ReviewCommandHandler>();
            services.AddScoped<IRequestHandler<CreateReadingCommand, CustomResult>, ReadingCommandHandler>();
            services.AddScoped<IReviewQueries, ReviewQueries>();
            services.AddScoped<IReadingQueries, ReadingQueries>();

            return services;
        }
    }
}
