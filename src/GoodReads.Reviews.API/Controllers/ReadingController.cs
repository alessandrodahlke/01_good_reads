using GoodReads.Core.Mediator;
using GoodReads.Reviews.Application.Commands;
using GoodReads.Reviews.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GoodReads.Reviews.API.Controllers
{
    [Route("api/reading")]
    [ApiController]
    public class ReadingController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ReadingController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReadingCommand command)
        {
            var result = await _mediatorHandler.EnviarComando(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReadingByUserId(Guid userId, [FromServices] IReadingQueries readingQueries)
        {
            var readings = await readingQueries.GetReadingByUserIdAsync(userId);

            if (readings.Any())
                return Ok(readings);

            return NotFound();
        }

    }
}
