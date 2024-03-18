using GoodReads.Core.Mediator;
using GoodReads.Core.Results;
using GoodReads.Reviews.Application.Commands;
using GoodReads.Reviews.Application.DTO;
using GoodReads.Reviews.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodReads.Reviews.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public UserController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("{id}/readings")]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateReading(Guid id, [FromBody] CreateReadingCommand command)
        {
            if (command.UserId != id)
                return BadRequest(CustomResult.Failure("Id not equals Command.Id"));

            var result = await _mediatorHandler.SendCommand(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}/readings")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetReadingsByUserId(Guid id, [FromServices] IUserQueries userQueries)
        {
            var reviews = await userQueries.GetByIdAsync(id);

            if (reviews is null)
                return NotFound();

            return Ok(reviews);
        }
    }
}
