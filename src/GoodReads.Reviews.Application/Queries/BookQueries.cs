using GoodReads.Reviews.Application.DTO;
using GoodReads.Reviews.Domain.Repositories;

namespace GoodReads.Reviews.Application.Queries
{
    public class BookQueries : IBookQueries
    {
        private readonly IBookRepository _bookRepository;

        public BookQueries(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookDTO> GetByIdAsync(Guid bookId)
        {
            var book = await _bookRepository.GetById(bookId.ToString());


            var bookDto = new BookDTO(book.Id, book.Description,book.Description, book.Author, 
                book.Reviews.Select(r => new ReviewDTO(r.Id, r.Description, r.BookId, r.UserId, r.Grade, r.CreatedAt)).ToList());

            return bookDto;
        }
    }
}
