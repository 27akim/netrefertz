using AutoMapper;
using BooksManagement.Application.Services;
using BooksManagement.Core.Entities;
using BooksManagement.Core.Interfaces.Repositories;
using BooksManagement.Core.Models.Requests;
using Moq;
using Xunit;

namespace BooksManagementTests.Services;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _mockMapper = new Mock<IMapper>();
        _bookService = new BookService(_mockBookRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedBook()
    {
        // Arrange
        var request = new CreateBookRequest { Title = "Test Title", Author = "Test Author", PublishedDate = DateTime.UtcNow, ISBN = "1234567890" };
        var book = new Book { Id = "1", Title = request.Title, Author = request.Author, PublishedDate = request.PublishedDate, ISBN = request.ISBN };

        _mockMapper.Setup(m => m.Map<Book>(request)).Returns(book);
        _mockBookRepository.Setup(r => r.CreateAsync(book)).ReturnsAsync(book);

        // Act
        var result = await _bookService.CreateAsync(request);

        // Assert
        Assert.Equal(book, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenBookExists()
    {
        // Arrange
        var bookId = "1";
        var book = new Book { Id = bookId, Title = "Test Title", Author = "Test Author", PublishedDate = DateTime.UtcNow, ISBN = "1234567890" };

        _mockBookRepository.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(book);
        _mockBookRepository.Setup(r => r.DeleteAsync(book)).Returns(Task.CompletedTask);

        // Act
        var result = await _bookService.DeleteAsync(bookId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = "1";

        _mockBookRepository.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync((Book)null);

        // Act
        var result = await _bookService.DeleteAsync(bookId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfBooks()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book { Id = "1", Title = "Test Title 1", Author = "Test Author 1", PublishedDate = DateTime.UtcNow, ISBN = "1234567890" },
            new Book { Id = "2", Title = "Test Title 2", Author = "Test Author 2", PublishedDate = DateTime.UtcNow, ISBN = "0987654321" }
        };

        _mockBookRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

        // Act
        var result = await _bookService.GetAllAsync();

        // Assert
        Assert.Equal(books, result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnBook_WhenBookExists()
    {
        // Arrange
        var bookId = "1";
        var book = new Book { Id = bookId, Title = "Test Title", Author = "Test Author", PublishedDate = DateTime.UtcNow, ISBN = "1234567890" };

        _mockBookRepository.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(book);

        // Act
        var result = await _bookService.GetByIdAsync(bookId);

        // Assert
        Assert.Equal(book, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedBook()
    {
        // Arrange
        var request = new UpdateBookRequest { Id = "1", Title = "Updated Title", Author = "Updated Author", PublishedDate = DateTime.UtcNow, ISBN = "1234567890" };
        var book = new Book { Id = request.Id, Title = request.Title, Author = request.Author, PublishedDate = request.PublishedDate, ISBN = request.ISBN };

        _mockMapper.Setup(m => m.Map<Book>(request)).Returns(book);
        _mockBookRepository.Setup(r => r.UpdateAsync(book)).ReturnsAsync(book);

        // Act
        var result = await _bookService.UpdateAsync(request);

        // Assert
        Assert.Equal(book, result);
    }
}
