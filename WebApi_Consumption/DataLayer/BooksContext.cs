namespace DataLayer
{
    using DataLayer.Models;
    using System.Data.Entity;

    public class BooksContext : DbContext
    {
        public BooksContext(string connectionString) : base(connectionString) { }

        public BooksContext()
            // :base(@"data source=DESKTOP-7LUT7NB\SQLEXPRESS;initial catalog=Books;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            : base("name=BooksContextAlternate")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            
        }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

}
}