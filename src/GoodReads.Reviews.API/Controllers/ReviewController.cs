using GoodReads.Core.Mediator;
using GoodReads.Reviews.Application.Commands;
using GoodReads.Reviews.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GoodReads.Reviews.API.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ReviewController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
        {
            var result = await _mediatorHandler.EnviarComando(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReviewsByBookId(Guid bookId, [FromServices] IReviewQueries reviewQueries)
        {
            var reviews = await reviewQueries.GetReviewsByBookIdAsync(bookId);

            if (reviews.Any())
                return Ok(reviews);

            return NotFound();
        }

    }
}
