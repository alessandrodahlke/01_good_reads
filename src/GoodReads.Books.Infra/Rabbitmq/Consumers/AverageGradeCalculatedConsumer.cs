using GoodReads.Books.Domain.Repositories;
using GoodReads.Core.Messages.Integration;
using MassTransit;

namespace GoodReads.Books.Infra.Rabbitmq.Consumers
{
    public class AverageGradeCalculatedConsumer : IConsumer<AverageGradeCalculatedEvent>
    {
        private readonly IBookRepository _bookRepository;

        public AverageGradeCalculatedConsumer(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Consume(ConsumeContext<AverageGradeCalculatedEvent> context)
        {
            var book = await _bookRepository.GetById(context.Message.BookId);

            book.UpdateAverageGrade(context.Message.Average);

            _bookRepository.Update(book);

            await _bookRepository.UnitOfWork.Commit();
        }
    }
}
