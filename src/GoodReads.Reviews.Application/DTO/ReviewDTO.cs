namespace GoodReads.Reviews.Application.DTO
{
    public record ReviewDTO(string Id, string Description, string BookId, string UserId, DateTime CreatedAt);
}
