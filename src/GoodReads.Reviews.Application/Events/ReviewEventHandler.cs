using GoodReads.Core.MessageBus;
using GoodReads.Core.Messages.Integration;
using GoodReads.Reviews.Domain.Repositories;
using MediatR;

namespace GoodReads.Reviews.Application.Events
{
    public class ReviewEventHandler : INotificationHandler<ReviewCreatedEvent>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMessageBus _messageBus;
        public ReviewEventHandler(IBookRepository bookRepository, IMessageBus messageBus)
        {
            _bookRepository = bookRepository;
            _messageBus = messageBus;
        }
        public async Task Handle(ReviewCreatedEvent notification, CancellationToken cancellationToken)
        {
            //Calcular nota media e publicar evento de integração para demais contextos
            var book = await _bookRepository.GetById(notification.BookId);

            var average = book.CalculateAverageGrade();

            await _messageBus.PublishAsync(new AverageGradeCalculatedEvent(Guid.Parse(book.Id), average), cancellationToken);
        }
    }
}
