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
