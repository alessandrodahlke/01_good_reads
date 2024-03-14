using GoodReads.Core.Data;
using GoodReads.Core.Messages.Integration;
using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MediatR;

namespace GoodReads.Reviews.Application.Events.Handlers
{
    public class BookCreatedIntegrationEventHandler : INotificationHandler<BookCreatedIntegrationEvent>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookCreatedIntegrationEventHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(BookCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await _bookRepository.Add(new Book(notification.Id.ToString(),
                    notification.Title, notification.Description, notification.Author));

                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
