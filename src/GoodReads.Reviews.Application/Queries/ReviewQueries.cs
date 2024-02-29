using GoodReads.Reviews.Application.DTO;
using GoodReads.Reviews.Domain.Repositories;

namespace GoodReads.Reviews.Application.Queries
{
    public class ReviewQueries : IReviewQueries
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewQueries(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByBookIdAsync(Guid bookId)
        {
            var reviews = await _reviewRepository.GetByBookId(bookId);

            return reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                BookId = r.BookId,
                UserId = r.UserId,
                Description = r.Description,
                Grade = r.Grade,
                CreatedAt = r.CreatedAt,
            });
        }
    }
}
