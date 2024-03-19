using GoodReads.Reviews.Application.DTO;
using GoodReads.Reviews.Domain.Repositories;

namespace GoodReads.Reviews.Application.Queries
{
    public class BookQueries : IBookQueries
    {
        private readonly IBookRepository _bookRepository;
        private readonly IRatingRepository _ratingRepository;

        public BookQueries(IBookRepository bookRepository, IRatingRepository ratingRepository)
        {
            _bookRepository = bookRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<RatingDTO> GetByIdAsync(Guid id)
        {
            var rating = await _ratingRepository.GetById(id.ToString());

            if (rating is null)
                return null;

            var bookDto = new RatingDTO(rating.Id, rating.Grade, rating.Description, rating.UserId, rating.BookId, rating.CreatedAt);

            return bookDto;
        }

        public async Task<IEnumerable<RatingDTO>> GetRatingsByBookIdAsync(Guid bookId)
        {
            var ratings = await _ratingRepository.GetByBookId(bookId.ToString());

            if (ratings is null)
                return null;

            var ratingsDto = ratings.Select(r => new RatingDTO(r.Id, r.Grade, r.Description, r.UserId, r.BookId, r.CreatedAt)).ToList();

            return ratingsDto;
        }

        public async Task<IEnumerable<RatingDTO>> GetRatingsByUserIdAsync(Guid userId)
        {
            var ratings = await _ratingRepository.GetByUserId(userId.ToString());

            if (ratings is null)
                return null;

            var ratingsDto = ratings.Select(r => new RatingDTO(r.Id, r.Grade, r.Description, r.UserId, r.BookId, r.CreatedAt)).ToList();

            return ratingsDto;
        }

    }
}