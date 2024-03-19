using GoodReads.Reviews.Application.DTO;

namespace GoodReads.Reviews.Application.Queries
{
    public interface IBookQueries
    {
        Task<RatingDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<RatingDTO>> GetRatingsByBookIdAsync(Guid bookId);
        Task<IEnumerable<RatingDTO>> GetRatingsByUserIdAsync(Guid userId);
    }
}
