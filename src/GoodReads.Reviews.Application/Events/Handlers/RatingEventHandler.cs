using GoodReads.Core.MessageBus;
using GoodReads.Core.Messages.Integration;
using GoodReads.Reviews.Domain.Repositories;
using MediatR;

namespace GoodReads.Reviews.Application.Events.Handlers
{
    public class RatingEventHandler : INotificationHandler<RatingCreatedEvent>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMessageBus _messageBus;
        public RatingEventHandler(IBookRepository bookRepository, IMessageBus messageBus)
        {
            _bookRepository = bookRepository;
            _messageBus = messageBus;
        }
        public async Task Handle(RatingCreatedEvent notification, CancellationToken cancellationToken)
        {
            //Calcular nota media e publicar evento de integração para demais contextos
            var book = await _bookRepository.GetById(notification.BookId);

            await _messageBus.PublishAsync(new AverageGradeCalculatedIntegrationEvent(Guid.Parse(book.Id), book.AverageGrade), cancellationToken);
        }
    }
}
