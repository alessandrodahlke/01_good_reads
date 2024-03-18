using GoodReads.Core.Mediator;
using GoodReads.Core.Results;
using GoodReads.Users.Application.Commands;
using GoodReads.Users.Application.DTO;
using GoodReads.Users.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodReads.Users.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserQueries _usersQueries;

        public UserController(IMediatorHandler mediatorHandler, IUserQueries usersQueries)
        {
            _mediatorHandler = mediatorHandler;
            _usersQueries = usersQueries;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediatorHandler.SendCommand(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _usersQueries.GetUserById(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
