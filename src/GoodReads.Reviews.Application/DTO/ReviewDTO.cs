namespace GoodReads.Reviews.Application.DTO
{
    public class ReviewDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public int Grade { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
