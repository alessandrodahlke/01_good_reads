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

            if (book is null)
                return null;

            var bookDto = new BookDTO(book.Id, book.Title, book.Description, book.AverageGrade, book.Author,
                book.Reviews.Select(r => new ReviewDTO(r.Id, r.Description, r.BookId, r.UserId, r.CreatedAt)).ToList(),
                book.Ratings.Select(r => new RatingDTO(r.Id, r.Grade, r.BookId, r.UserId, r.CreatedAt)).ToList());

            return bookDto;
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByBookIdAsync(Guid bookId)
        {
            var book = await _bookRepository.GetById(bookId.ToString());

            if (book is null)
                return null;

            var reviewsDto = book.Reviews.Select(r => new ReviewDTO(r.Id, r.Description, r.BookId, r.UserId, r.CreatedAt)).ToList();

            return reviewsDto;
        }

        public async Task<IEnumerable<RatingDTO>> GetRatingsByBookIdAsync(Guid bookId)
        {
            var book = await _bookRepository.GetById(bookId.ToString());

            if (book is null)
                return null;

            var ratingsDto = book.Ratings.Select(r => new RatingDTO(r.Id, r.Grade, r.BookId, r.UserId, r.CreatedAt)).ToList();

            return ratingsDto;
        }
    }
}