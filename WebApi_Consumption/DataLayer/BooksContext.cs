namespace DataLayer
{
    using DataLayer.Models;
    using System.Data.Entity;

    public class BooksContext : DbContext
    {
        public BooksContext()
            : base("name=BooksContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

    }
}