using GoodReads.Reviews.Application.DTO;

namespace GoodReads.Reviews.Application.Queries
{
    public interface IBookQueries
    {
        Task<BookDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<ReviewDTO>> GetReviewsByBookIdAsync(Guid id);
        Task<IEnumerable<RatingDTO>> GetRatingsByBookIdAsync(Guid id);
    }
}
