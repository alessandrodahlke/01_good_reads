namespace GoodReads.Reviews.Application.DTO
{
    public record BookDTO(string Id, string Title, string Description, string Author, List<ReviewDTO> Reviews);
}
