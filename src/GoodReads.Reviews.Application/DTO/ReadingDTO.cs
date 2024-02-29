namespace GoodReads.Reviews.Application.DTO
{
    public record ReadingDTO(string Id, Guid BookId, Guid UserId, DateTime StartedDate, DateTime? EndedDate);
}
