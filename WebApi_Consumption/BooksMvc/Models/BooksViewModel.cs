using DataLayer.Models;
using System.Collections.Generic;

namespace BooksMvc.Models
{
    public class BooksViewModel
    {
        public List<Book> Books { get; set; }
        public Author SelectedAuthor { get; set; }
    }
}