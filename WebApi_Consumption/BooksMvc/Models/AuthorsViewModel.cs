using DataLayer.Models;
using System.Collections.Generic;

namespace BooksMvc.Models
{
    public class AuthorsViewModel
    {
        public List<Author> Authors { get; set; }
        public Book SelectedBook { get; set; }
    }
}