using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
