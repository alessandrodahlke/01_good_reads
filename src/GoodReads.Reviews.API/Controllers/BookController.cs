using GoodReads.Core.Mediator;
using GoodReads.Core.Results;
using GoodReads.Reviews.Application.Commands;
using GoodReads.Reviews.Application.DTO;
using GoodReads.Reviews.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodReads.Reviews.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public BookController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("{id}/reviews")]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateReview(Guid id, [FromBody] CreateReviewCommand command)
        {
            if (command.BookId != id)
                return BadRequest(CustomResult.Failure("Id not equals Command.Id"));

            var result = await _mediatorHandler.SendCommand(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}/reviews")]
        [ProducesResponseType(typeof(BookDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetReviewsByBookId(Guid id, [FromServices] IBookQueries bookQueries)
        {
            var reviews = await bookQueries.GetByIdAsync(id);

            if (reviews is null)
                return NotFound();

            return Ok(reviews);
        }

        [HttpGet("{bookId}/users/{userId}/reviews")]
        [ProducesResponseType(typeof(BookDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetReviewByBookIdAndUserId(Guid bookId, Guid userId, [FromServices] IBookQueries bookQueries)
        {
            var reviews = await bookQueries.GetReviewByBookIdAndUserId(bookId, userId);

            if (reviews is null)
                return NotFound();

            return Ok(reviews);
        }

        [HttpGet("reviews/{id}")]
        [ProducesResponseType(typeof(BookDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetReviewById(Guid id, [FromServices] IBookQueries bookQueries)
        {
            var reviews = await bookQueries.GetReviewById(id);

            if (reviews is null)
                return NotFound();

            return Ok(reviews);
        }

        [HttpPost("{id}/ratings")]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateRating(Guid id, [FromBody] CreateRatingCommand command)
        {
            if (command.BookId != id)
                return BadRequest(CustomResult.Failure("Id not equals Command.Id"));

            var result = await _mediatorHandler.SendCommand(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}/ratings")]
        [ProducesResponseType(typeof(BookDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRatingsByBookId(Guid id, [FromServices] IBookQueries bookQueries)
        {
            var reviews = await bookQueries.GetByIdAsync(id);

            if (reviews is null)
                return NotFound();

            return Ok(reviews);
        }
    }
}
