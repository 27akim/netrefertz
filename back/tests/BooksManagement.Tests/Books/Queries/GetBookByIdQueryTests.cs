using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Application.Books.Queries.GetById;
using BooksManagement.Core.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BooksManagement.UnitTests.Books.Queries
{
    public class GetBookByIdQueryTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly GetBookByIdQueryHandler _handler;
        private readonly Mock<ILogger<GetBookByIdQueryHandler>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;

        public GetBookByIdQueryTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _mockLogger = new Mock<ILogger<GetBookByIdQueryHandler>>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetBookByIdQueryHandler(_mockMapper.Object, _mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnBook()
        {
            // Arrange
            var query = new GetBookByIdQuery
            {
                Id = "1"
            };
            var book = new Book
            {
                Id = "1",
                Title = "Test Title",
                Author = "Test Author",
                PublishedDate = DateTime.UtcNow,
                ISBN = "1234567890"
            };
            _mockRepository.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync(book);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Test Title", result.Title);
            Assert.Equal("Test Author", result.Author);
            Assert.Equal("1234567890", result.ISBN);
        }

        [Fact]
        public async Task Handle_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            var query = new GetBookByIdQuery
            {
                Id = "non-existent-id"
            };
            _mockRepository.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync((Book)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
