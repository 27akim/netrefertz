using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Application.Books.Commands;
using BooksManagement.Core.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BooksManagement.UnitTests.Books.Commands
{
    public class UpdateBookCommandTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateBookHandler _handler;
        private readonly Mock<ILogger<UpdateBookHandler>> _mockLogger;

        public UpdateBookCommandTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<UpdateBookHandler>>();
            _handler = new UpdateBookHandler(_mockMapper.Object, _mockRepository.Object);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldUpdateBook()
        {
            // Arrange
            var command = new UpdateBookCommand
            {
                Id = "1",
                Title = "New Title",
                Author = "New Author",
                PublishedDate = DateTime.UtcNow,
                ISBN = "1234567890"
            };
            var cancellationToken = new CancellationToken();
            var book = new Book
            {
                Id = command.Id,
                Title = command.Title,
                Author = command.Author,
                PublishedDate = command.PublishedDate,
                ISBN = command.ISBN
            };
            _mockMapper.Setup(m => m.Map<Book>(command)).Returns(book);
            _mockRepository.Setup(r => r.UpdateAsync(book)).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(It.Is<Book>(b =>
                b.Id == command.Id &&
                b.Title == command.Title &&
                b.Author == command.Author &&
                b.PublishedDate == command.PublishedDate &&
                b.ISBN == command.ISBN
            )), Times.Once);
        }
    }
}
