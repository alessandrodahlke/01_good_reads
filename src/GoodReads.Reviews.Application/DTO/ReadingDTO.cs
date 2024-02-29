namespace GoodReads.Reviews.Application.DTO
{
    public class ReadingDTO
    {
        public string Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime? EndedDate { get; set; }
    }
}
