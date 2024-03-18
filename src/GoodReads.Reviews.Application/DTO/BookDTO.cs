namespace GoodReads.Reviews.Application.DTO
{
    public record BookDTO(string Id, string Title, string Description, decimal AverageGrade, string Author, List<ReviewDTO> Reviews = null, List<RatingDTO> Ratings = null);
}
