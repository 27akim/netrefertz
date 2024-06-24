namespace BooksManagement.Core.Models.Requests
{
    public class CreateBookRequest
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string ISBN { get; set; }
    }
}
