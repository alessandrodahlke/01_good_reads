using GoodReads.Books.Application.Events;
using GoodReads.Books.Domain.Entities;
using GoodReads.Books.Domain.Repositories;
using GoodReads.Core.Messages;
using GoodReads.Core.Results;
using MediatR;
using static GoodReads.Books.Application.Events.BookUpdatedEvent;

namespace GoodReads.Books.Application.Commands
{

    public class BookCommandHandler : CommandHandler,
        IRequestHandler<CreateBookCommand, CustomResult>,
        IRequestHandler<UpdateBookCommand, CustomResult>,
        IRequestHandler<DeleteBookCommand, CustomResult>
    {
        private readonly IBookRepository _bookRepository;

        public BookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<CustomResult> Handle(CreateBookCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return CustomResult.Failure("Invalid command", message.GetErrors());

            if (await InUse(message.ISBN))
                return CustomResult.Failure("This ISBN is already in use.", GetErrors());

            var book = new Book(message.Title, message.Description, message.ISBN, message.Author, message.Publisher, message.Gender, message.Year, message.NumberOfPages);
            await _bookRepository.Add(book);

            book.AddEvent(new BookCreatedEvent(book.Id, book.Title, book.Author, book.ISBN, book.Publisher, book.Gender, book.Year, book.NumberOfPages));

            var result = await _bookRepository.UnitOfWork.Commit();

            return CustomResult.Success("Book created successfully",
                new CreateBookResponse(book.Id, book.Title, book.Description, book.ISBN, book.Author, book.Publisher, book.Gender, book.Year, book.NumberOfPages));
        }

        public async Task<CustomResult> Handle(UpdateBookCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return CustomResult.Failure("Invalid command", message.GetErrors());

            var book = await _bookRepository.GetById(message.Id);

            if (book == null)
            {
                AddError("The book does not exist.");
                return CustomResult.Failure("The book does not exist", GetErrors());
            }

            book.UpdateBook(message.Title, message.Description, message.Author, message.Publisher, message.Gender, message.Year, message.NumberOfPages);

            _bookRepository.Update(book);

            book.AddEvent(new BookUpdatedEvent(book.Id, book.Title, book.Author, book.ISBN, book.Publisher, book.Gender, book.Year, book.NumberOfPages));

            var result = await _bookRepository.UnitOfWork.Commit();
            if (result)
                return CustomResult.Success("Successfully Updated");

            return CustomResult.Failure("Updated Unsuccessful", GetErrors());
        }

        public async Task<CustomResult> Handle(DeleteBookCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return CustomResult.Failure("Invalid command", GetErrors());


            var book = await _bookRepository.GetById(message.Id);

            if (book == null)
            {
                AddError("The book does not exist.");
                return CustomResult.Failure("The book does not exist.", GetErrors());
            }

            _bookRepository.Remove(book);

            book.AddEvent(new BookDeletedEvent(book.Id));

            var result = await _bookRepository.UnitOfWork.Commit();

            if (result)
                return CustomResult.Success("Successfully deleted");

            return CustomResult.Failure("Deleted Unsucessfull", GetErrors());
        }

        private async Task<bool> InUse(string isbn)
        {
            var bookExists = await _bookRepository.GetByISBN(isbn);

            if (bookExists != null)
            {
                AddError("This ISBN is already in use.");
                return true;
            }
            return false;
        }
    }
}
