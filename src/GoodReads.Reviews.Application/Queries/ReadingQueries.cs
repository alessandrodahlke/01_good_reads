using GoodReads.Reviews.Application.DTO;
using GoodReads.Reviews.Domain.Repositories;

namespace GoodReads.Reviews.Application.Queries
{
    public class ReadingQueries : IReadingQueries
    {
        private readonly IReadingRepository _readingRepository;

        public ReadingQueries(IReadingRepository reviewRepository)
        {
            _readingRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReadingDTO>> GetReadingByUserIdAsync(Guid userId)
        {
            var readings = await _readingRepository.GetByUserId(userId);

            return readings.Select(r => new ReadingDTO(r.Id, r.BookId, r.UserId, r.StartedDate, r.EndedDate));
        }
    }
}
