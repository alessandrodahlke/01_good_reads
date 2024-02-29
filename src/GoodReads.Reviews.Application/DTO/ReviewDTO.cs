namespace GoodReads.Reviews.Application.DTO
{
    public record ReviewDTO(string Id, string Description, Guid BookId, Guid UserId, int Grade, DateTime CreatedAt);
}
