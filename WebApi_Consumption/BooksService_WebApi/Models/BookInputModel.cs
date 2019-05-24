using DataLayer.Models;

namespace BooksService_WebApi.Models
{
    public class BookInputModel
    {
        public string Name { get; set; }
        public int PublicationYear { get; set; }
        public Author Author { get; set; }
    }
}