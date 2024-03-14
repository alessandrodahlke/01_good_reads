using GoodReads.Books.Application.Commands;
using GoodReads.Books.Application.DTO;
using GoodReads.Books.Application.Queries;
using GoodReads.Core.Mediator;
using GoodReads.Core.Results;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace GoodReads.Books.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediatorHandler _mediator;

        public BookController(IMediatorHandler mediator, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command)
        {
            _logger.LogInformation(
                "Creating a new book: {@command}", command);

            var result = await _mediator.SendCommand(command);

            _logger.LogInformation(
                "Book created: {@result}", result);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookCommand command)
        {
            if (command.Id != id)
                return BadRequest(CustomResult.Failure("Id not equals Command.Id"));

            var result = await _mediator.SendCommand(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteBookCommand(id);
            var result = await _mediator.SendCommand(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById([FromServices] IBookQueries queries, Guid id)
        {
            var book = await queries.GetById(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(PagedResult<BookDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll([FromServices] IBookQueries queries, [FromQuery] int size = 10, [FromQuery] int index = 1)
        {
            return Ok(await queries.GetAll(size, index));
        }
    }
}
