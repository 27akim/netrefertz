using HotChocolate.Types;
using HotChocolate;

namespace BooksManagement.Core.Entities
{
    public class Book
    {
        [GraphQLType(typeof(NonNullType<IdType>))]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Title { get; set; }
        public required string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string ISBN { get; set; }
    }
}
