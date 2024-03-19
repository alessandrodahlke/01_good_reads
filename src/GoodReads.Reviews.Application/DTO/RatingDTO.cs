namespace GoodReads.Reviews.Application.DTO
{
    public record RatingDTO(string Id, int Grade, string Description, string UserId, string BookId, DateTime CreatedAt);
}
