using GoodReads.Books.Domain.Repositories;
using GoodReads.Core.Messages.Integration;
using MediatR;

namespace GoodReads.Books.Application.Commands.Handlers
{
    public class AverageGradeCalculatedIntegrationHandler : INotificationHandler<AverageGradeCalculatedIntegrationEvent>
    {
        private readonly IBookRepository _bookRepository;

        public AverageGradeCalculatedIntegrationHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Handle(AverageGradeCalculatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetById(notification.BookId);

            book.UpdateAverageGrade(notification.Average);

            _bookRepository.Update(book);

            await _bookRepository.UnitOfWork.Commit();
        }
    }
}
