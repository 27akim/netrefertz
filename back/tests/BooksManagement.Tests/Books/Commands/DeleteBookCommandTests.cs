using BooksManagement.Abstractions;
using BooksManagement.Application.Books.Commands.Delete;
using BooksManagement.Core.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BooksManagement.UnitTests.Books.Commands
{
    public class DeleteBookCommandTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly DeleteBookHandler _handler;
        private readonly Mock<ILogger<DeleteBookHandler>> _mockLogger;

        public DeleteBookCommandTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _mockLogger = new Mock<ILogger<DeleteBookHandler>>();
            _mockLogger = new Mock<ILogger<DeleteBookHandler>>();
            _handler = new DeleteBookHandler(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenBookExists()
        {
            // Arrange
            var command = new DeleteBookCommand
            {
                Id = "1"
            };

            var book = new Book
            {
                Id = "1",
                Title = "Test Title",
                Author = "Test Author",
                PublishedDate = DateTime.Now,
                ISBN = "1234567890"
            };

            _mockRepository.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(book);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Arrange
            var command = new DeleteBookCommand
            {
                Id = "1"
            };

            _mockRepository.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync((Book)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
