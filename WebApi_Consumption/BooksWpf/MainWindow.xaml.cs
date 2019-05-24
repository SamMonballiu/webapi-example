using DataLayer;
using DataLayer.Models;
using OrchestrationLayer;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;

namespace BooksWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static DbContext ctx = new BooksContext();
        IRepository<Book> books = new Repository<Book>(ctx);
        IRepository<Author> authors = new Repository<Author>(ctx);

        public MainWindow()
        {
            InitializeComponent();


            cbAuthors.DisplayMemberPath = "Name";
            cbBooks.DisplayMemberPath = "Name";
            cbAuthors.SelectedValuePath = "Id";
            cbBooks.SelectedValuePath = "Id";

            UpdateComboBoxes();
        }

        private async void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewAuthor.Text == String.Empty) { return; }
            try
            {
                var service = new BooksService("http://localhost:51518/");
                var response = await service.AddAuthor(txtNewAuthor.Text);
                if (response.IsSuccessStatusCode)
                {
                    UpdateComboBoxes();
                    txtNewAuthor.Text = String.Empty;
                }

                else
                {
                    MessageBox.Show("An error occured: " + response.ReasonPhrase);
                }
            }
            catch (HttpRequestException requestException) { MessageBox.Show(requestException.InnerException.Message, "Error"); }
            catch (Exception ex) { MessageBox.Show("An error occurred: " + ex.Message); }

        }



        private void UpdateComboBoxes()
        {
            cbBooks.ItemsSource = books.GetAll().ToList();
            cbAuthors.ItemsSource = authors.GetAll().Include(x => x.Books).ToList();
        }

        private async Task AddAuthor(string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51518/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var newAuthor = new Author() { Name = name };

                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/Books/CreateAuthor", newAuthor);
                    if (response.IsSuccessStatusCode)
                    {
                        UpdateComboBoxes();
                        txtNewAuthor.Text = String.Empty;
                    }

                    else
                    {
                        MessageBox.Show("An error occured: " + response.ReasonPhrase);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

            }

        }

        private async Task CreateNewBookWithAuthor(Author author, string bookName)
        {
            var newBook = new Book()
            {
                AuthorId = author.Id,
                Name = bookName,
                PublicationYear = 2000
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51518/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Books/AddBookWithAuthor", newBook);

                if (response.IsSuccessStatusCode)
                {
                    UpdateComboBoxes();
                    txtNewBook.Text = String.Empty;
                }

            }
        }

        private async void btnNewBook_Click(object sender, RoutedEventArgs e)
        {
            var newBookName = txtNewBook.Text;
            var author = (Author)cbAuthors.SelectedItem;
            await CreateNewBookWithAuthor(author, newBookName);
        }
    }
}
