using BooksManagement.Core.Interfaces.Services;
using BooksManagement.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BooksManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
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
            var books = await _bookService.GetAllAsync();
            return Ok(books);
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
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        /// <summary>Creates new book.</summary>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Returns if the book is created.</response>
        /// <response code="500">Returns if an unhandled error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateBookRequest request)
        {
            var book = await _bookService.CreateAsync(request);
            return Ok(book);
        }

        /// <summary>Updates an existing book.</summary>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Returns if the book is successfully updated.</response>
        /// <response code="500">Returns if an unhandled error occurs.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UpdateBookRequest request)
        {
            var result = await _bookService.UpdateAsync(request);
            return Ok(result);
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
            if(await _bookService.DeleteAsync(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
