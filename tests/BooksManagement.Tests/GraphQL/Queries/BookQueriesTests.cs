using BooksManagement.Application.GraphQL.Queries;
using BooksManagement.Core.Entities;
using BooksManagement.Core.Interfaces.Repositories;
using Moq;
using Xunit;

namespace BooksManagement.Tests.GraphQL.Queries;

public class BookQueriesTests
{
    [Fact]
    public async Task GetBooksAsync_ShouldReturnAllBooks()
    {
        // Arrange
        var expectedBooks = new List<Book>
        {
            new Book { Id = "1", Title = "Book 1", Author = "Author 1", PublishedDate = DateTime.UtcNow, ISBN = "1234567890" },
            new Book { Id = "2", Title = "Book 2", Author = "Author 2", PublishedDate = DateTime.UtcNow, ISBN = "0987654321" }
        };

        var mockRepository = new Mock<IBookRepository>();
        mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedBooks);

        var bookQueries = new BookQueries();

        // Act
        var result = await bookQueries.GetBooksAsync(mockRepository.Object);

        // Assert
        Assert.Equal(expectedBooks.Count, result.Count);
        Assert.Equal(expectedBooks.First().Title, result.First().Title);
        Assert.Equal(expectedBooks.Last().Title, result.Last().Title);
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnCorrectBook()
    {
        // Arrange
        var bookId = "1";
        var expectedBook = new Book { Id = bookId, Title = "Book 1", Author = "Author 1", PublishedDate = DateTime.UtcNow, ISBN = "1234567890" };

        var mockRepository = new Mock<IBookRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(expectedBook);

        var bookQueries = new BookQueries();

        // Act
        var result = await bookQueries.GetBookByIdAsync(mockRepository.Object, bookId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedBook.Title, result.Title);
        Assert.Equal(expectedBook.Author, result.Author);
        Assert.Equal(expectedBook.PublishedDate, result.PublishedDate);
        Assert.Equal(expectedBook.ISBN, result.ISBN);
    }
}

