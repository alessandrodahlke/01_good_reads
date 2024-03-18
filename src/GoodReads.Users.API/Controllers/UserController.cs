using GoodReads.Core.Mediator;
using GoodReads.Users.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace GoodReads.Users.API.Controllers
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediatorHandler.SendCommand(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
