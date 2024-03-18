namespace GoodReads.Reviews.Application.DTO
{
    public record ReadingDTO(string Id, BookDTO Book, DateTime StartedDate, DateTime? EndedDate);
}
