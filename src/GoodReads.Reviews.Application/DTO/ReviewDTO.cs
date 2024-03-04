namespace GoodReads.Reviews.Application.DTO
{
    public record ReviewDTO(string Id, string Description, string BookId, string UserId, int Grade, DateTime CreatedAt);
}
