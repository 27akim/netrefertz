using Xunit;
using Moq;
using AutoMapper;
using BooksManagement.Application.GraphQL.Mutations;
using BooksManagement.Core.Entities;
using BooksManagement.Core.Interfaces.Repositories;
using BooksManagement.Core.Models.Requests;

namespace BooksManagement.Tests.GraphQL.Mutations;

public class BookMutationsTests
{
    private readonly Mock<IBookRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly BookMutations _bookMutations;

    public BookMutationsTests()
    {
        _repositoryMock = new Mock<IBookRepository>();
        _mapperMock = new Mock<IMapper>();
        _bookMutations = new BookMutations();
    }

    [Fact]
    public async Task CreateBookAsync_ShouldReturnCreatedBook()
    {
        // Arrange
        var request = new CreateBookRequest { Title = "Test Title", Author = "Test Author", ISBN = "1234567890", PublishedDate = DateTime.Now };
        var book = new Book { Id = "1", Title = request.Title, Author = request.Author, ISBN = request.ISBN, PublishedDate = request.PublishedDate };

        _mapperMock.Setup(m => m.Map<Book>(request)).Returns(book);
        _repositoryMock.Setup(r => r.CreateAsync(book)).ReturnsAsync(book);

        // Act
        var result = await _bookMutations.CreateBookAsync(_repositoryMock.Object, _mapperMock.Object, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(book.Id, result.Id);
        Assert.Equal(book.Title, result.Title);
    }

    [Fact]
    public async Task DeleteBookAsync_ShouldReturnTrue_WhenBookExists()
    {
        // Arrange
        var bookId = "1";
        var book = new Book { Id = bookId, Title = "Test Title", Author = "Test Author", ISBN = "1234567890", PublishedDate = DateTime.Now };

        _repositoryMock.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(book);
        _repositoryMock.Setup(r => r.DeleteAsync(book)).Returns(Task.CompletedTask);

        // Act
        var result = await _bookMutations.DeleteBookAsync(_repositoryMock.Object, bookId);

        // Assert
        Assert.True(result);
        _repositoryMock.Verify(r => r.DeleteAsync(book), Times.Once);
    }

    [Fact]
    public async Task DeleteBookAsync_ShouldReturnFalse_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = "1";

        _repositoryMock.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync((Book)null);

        // Act
        var result = await _bookMutations.DeleteBookAsync(_repositoryMock.Object, bookId);

        // Assert
        Assert.False(result);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task UpdateBookAsync_ShouldReturnUpdatedBook()
    {
        // Arrange
        var request = new UpdateBookRequest { Id = "1", Title = "Updated Title", Author = "Updated Author", ISBN = "0987654321", PublishedDate = DateTime.Now };
        var book = new Book { Id = request.Id, Title = request.Title, Author = request.Author, ISBN = request.ISBN, PublishedDate = request.PublishedDate };

        _mapperMock.Setup(m => m.Map<Book>(request)).Returns(book);
        _repositoryMock.Setup(r => r.UpdateAsync(book)).ReturnsAsync(book);

        // Act
        var result = await _bookMutations.UpdateBookAsync(_repositoryMock.Object, _mapperMock.Object, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(book.Id, result.Id);
        Assert.Equal(book.Title, result.Title);
    }
}
