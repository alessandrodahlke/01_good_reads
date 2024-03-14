using GoodReads.Core.Mediator;
using GoodReads.Core.Results;
using GoodReads.Reviews.Application.Commands;
using GoodReads.Reviews.Application.Queries;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReviewsByBookId(Guid id, [FromServices] IBookQueries reviewQueries)
        {
            var reviews = await reviewQueries.GetByIdAsync(id);

            if (reviews is null)
                return NotFound();

            return Ok(reviews);
        }

        [HttpPost("{id}/ratings")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRatingsByBookId(Guid id, [FromServices] IBookQueries reviewQueries)
        {
            var reviews = await reviewQueries.GetByIdAsync(id);

            if (reviews is null)
                return NotFound();

            return Ok(reviews);
        }

        //[HttpPost("{id}/readings")]
        //public async Task<IActionResult> CreateReading(Guid id, [FromBody] CreateReadingCommand command)
        //{
        //    if (command.BookId != id)
        //        return BadRequest(CustomResult.Failure("Id not equals Command.Id"));

        //    var result = await _mediatorHandler.EnviarComando(command);

        //    if (result.IsSuccess)
        //        return Ok(result);

        //    return BadRequest(result);
        //}

    }
}
