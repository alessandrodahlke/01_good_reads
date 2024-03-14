namespace GoodReads.Reviews.Application.DTO
{
    public record RatingDTO(string Id, int Grade, string BookId, string UserId, DateTime CreatedAt);
}
