using BooksManagement.Core.Entities;
using BooksManagement.Infrastructure.Data;
using BooksManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BooksManagement.Tests.Repositories;

public class BookRepositoryTests
{
    private readonly DbContextOptions<BookDataContext> _options;
    private readonly BookDataContext _context;
    private readonly BookRepository _repository;

    public BookRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<BookDataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BookDataContext(_options);
        _repository = new BookRepository(_context);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddBook()
    {
        // Arrange
        var book = new Book { Title = "Test Title", Author = "Test Author", ISBN = "1234567890123", PublishedDate = DateTime.Now };

        // Act
        var createdBook = await _repository.AddAsync(book);

        // Assert
        var dbBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == createdBook.Id);
        Assert.NotNull(dbBook);
        Assert.Equal("Test Title", dbBook.Title);
        Assert.Equal("Test Author", dbBook.Author);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveBook()
    {
        // Arrange
        var book = new Book { Title = "Test Title", Author = "Test Author", ISBN = "1234567890123", PublishedDate = DateTime.Now };
        await _repository.AddAsync(book);

        // Act
        await _repository.DeleteAsync(book);

        // Assert
        var dbBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
        Assert.Null(dbBook);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllBooks()
    {
        // Arrange
        var book1 = new Book { Title = "Test Title 1", Author = "Test Author 1", ISBN = "1234567890123", PublishedDate = DateTime.Now };
        var book2 = new Book { Title = "Test Title 2", Author = "Test Author 2", ISBN = "1234567890123", PublishedDate = DateTime.Now };
        await _repository.AddAsync(book1);
        await _repository.AddAsync(book2);

        // Act
        var books = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(2, books.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnBook()
    {
        // Arrange
        var book = new Book { Title = "Test Title", Author = "Test Author", ISBN = "1234567890123", PublishedDate = DateTime.Now };
        await _repository.AddAsync(book);

        // Act
        var foundBook = await _repository.GetByIdAsync(book.Id);

        // Assert
        Assert.NotNull(foundBook);
        Assert.Equal("Test Title", foundBook.Title);
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyBook()
    {
        // Arrange
        var book = new Book { Title = "Test Title", Author = "Test Author", ISBN = "1234567890123", PublishedDate = DateTime.Now };
        await _repository.AddAsync(book);

        // Act
        book.Title = "Updated Title";
        await _repository.UpdateAsync(book);

        // Assert
        var dbBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
        Assert.NotNull(dbBook);
        Assert.Equal("Updated Title", dbBook.Title);
    }
}
