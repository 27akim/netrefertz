using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Application.Books.Commands.Create;
using BooksManagement.Core.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BooksManagement.UnitTests.Books.Commands
{
    public class CreateBookCommandTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateBookHandler _handler;
        private readonly Mock<ILogger<CreateBookHandler>> _mockLogger;

        public CreateBookCommandTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CreateBookHandler>>();
            _handler = new CreateBookHandler(_mockMapper.Object, _mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnBookId()
        {
            // Arrange
            var command = new CreateBookCommand
            {
                Title = "Test Title",
                Author = "Test Author",
                PublishedDate = DateTime.Now,
                ISBN = "1234567890"
            };

            var book = new Book
            {
                Id = "1",
                Title = "Test Title",
                Author = "Test Author",
                PublishedDate = command.PublishedDate,
                ISBN = "1234567890"
            };

            _mockMapper.Setup(m => m.Map<Book>(command)).Returns(book);
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Book>())).ReturnsAsync(book);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Once);
            _mockMapper.Verify(m => m.Map<Book>(command), Times.Once);
            Assert.Equal("1", result);
        }
    }
}
