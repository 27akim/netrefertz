using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Application.Books.Queries.GetAll;
using BooksManagement.Core.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BooksManagement.UnitTests.Books.Queries
{
    public class GetAllBooksQueryTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly GetAllBooksQueryHandler _handler;
        private readonly Mock<ILogger<GetAllBooksQueryHandler>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;

        public GetAllBooksQueryTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _mockLogger = new Mock<ILogger<GetAllBooksQueryHandler>>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAllBooksQueryHandler(_mockMapper.Object, _mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllBooks()
        {
            // Arrange
            var query = new GetAllBooksQuery();
            var cancellationToken = new CancellationToken();
            var books = new List<Book>
        {
            new Book { Id = "1", Title = "Test Book 1", Author = "Author 1", PublishedDate = DateTime.UtcNow, ISBN = "1234567890" },
            new Book { Id = "2", Title = "Test Book 2", Author = "Author 2", PublishedDate = DateTime.UtcNow, ISBN = "0987654321" }
        };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, b => b.Id == "1" && b.Title == "Test Book 1");
            Assert.Contains(result, b => b.Id == "2" && b.Title == "Test Book 2");
        }

        [Fact]
        public async Task Handle_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var query = new GetAllBooksQuery();
            var cancellationToken = new CancellationToken();
            var books = new List<Book>();
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
