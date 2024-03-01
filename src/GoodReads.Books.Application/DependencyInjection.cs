using GoodReads.Books.Application.Commands;
using GoodReads.Books.Application.Events;
using GoodReads.Books.Application.Queries;
using GoodReads.Core.Mediator;
using GoodReads.Core.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoodReads.Books.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<CreateBookCommand, CustomResult>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBookCommand, CustomResult>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteBookCommand, CustomResult>, BookCommandHandler>();
            services.AddScoped<INotificationHandler<BookCreatedEvent>, BookEventHandler>();
            services.AddScoped<INotificationHandler<BookUpdatedEvent>, BookEventHandler>();
            services.AddScoped<INotificationHandler<BookDeletedEvent>, BookEventHandler>();
            services.AddScoped<IBookQueries, BookQueries>();

            return services;
        }
    }
}
