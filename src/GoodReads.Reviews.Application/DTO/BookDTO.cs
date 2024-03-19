namespace GoodReads.Reviews.Application.DTO
{
    public record BookDTO(string Id, string Title, string Description, decimal AverageGrade, string Author, List<RatingDTO> Ratings = null);
}
