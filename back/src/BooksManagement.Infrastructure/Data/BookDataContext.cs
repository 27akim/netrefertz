using BooksManagement.Abstractions;
using BooksManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksManagement.Infrastructure.Data;

public class BookDataContext : DbContext, IDataContext
{
    public BookDataContext(DbContextOptions<BookDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasIndex(x => x.Id)
            .IsUnique();

        modelBuilder.Entity<Book>().Property(og => og.ISBN).HasMaxLength(17);
    }

    public DbSet<Book> Books { get; set; }
}
