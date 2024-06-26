using BooksManagement.Application.Books.Commands;
using BooksManagement.Application.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IMediator mediator, ILogger<BooksController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>Returns all books.</summary>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Returns if books are found.</response>
        /// <response code="500">Returns if an unhandled error occurs.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("{action}", nameof(Get));
            var result = await _mediator.Send(new GetAllBooksQuery());
            return Ok(result);
        }

        /// <summary>Returns book.</summary>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Returns if the book is found.</response>
        /// <response code="404">Returns if the book is not found.</response>
        /// <response code="500">Returns if an unhandled error occurs.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            _logger.LogInformation("{action} : Id={id}", nameof(Get), id);
            var result = await _mediator.Send(new GetBookByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>Creates new book.</summary>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Returns if the book is created.</response>
        /// <response code="500">Returns if an unhandled error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateBookCommand command)
        {
            _logger.LogInformation("{action}", nameof(Post));
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>Updates an existing book.</summary>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Returns if the book is successfully updated.</response>
        /// <response code="500">Returns if an unhandled error occurs.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UpdateBookCommand command)
        {
            _logger.LogInformation("{action}", nameof(Put));
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>Deletes book.</summary>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="204">Returns if the book is successfully deleted.</response>
        /// <response code="404">Returns if the book is not found.</response>
        /// <response code="500">Returns if an unhandled error occurs.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("{action} : Id={id}", nameof(Delete), id);
            var result = await _mediator.Send(new DeleteBookCommand { Id = id });
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
