using DataLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OrchestrationLayer
{
    public class BooksService
    {
        private readonly string _baseUrl;
        public BooksService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        private void InitializeClient(HttpClient client)
        {
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Author>> GetAuthors()
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                try
                {
                    HttpResponseMessage response = client.GetAsync("api/Authors").Result;
                    var authors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Author>>(await response.Content.ReadAsStringAsync());
                    return authors;
                }
                catch (JsonSerializationException) { return null; }
                catch (Exception) { throw; }
            }
        }

        public async Task<List<Book>> GetBooks()
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                try
                {
                    HttpResponseMessage response = client.GetAsync("api/Books").Result;
                    var books = JsonConvert.DeserializeObject<List<Book>>(await response.Content.ReadAsStringAsync());
                    return books;
                }
                catch (JsonSerializationException) { return null; }
                catch (Exception) { throw; }
            }
        }

        public async Task<Book> GetBook(int id)
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                try
                {
                    HttpResponseMessage response = client.GetAsync("api/Books/"+ id).Result;
                    var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
                    return book;
                }
                catch (JsonSerializationException) { return null; }
                catch (Exception) { throw; }
            }
        }

        public async Task<Author> GetAuthor(int id)
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                try
                {
                    HttpResponseMessage response = client.GetAsync("api/Authors/" + id).Result;
                    var readString = await response.Content.ReadAsStringAsync();
                    var author = JsonConvert.DeserializeObject<Author>(readString);
                    return author;
                }
                //catch (JsonSerializationException ex) { return null; }
                catch (Exception) { throw; }
            }
        }

        public async Task<HttpResponseMessage> CreateAuthor(string name)
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);
                var newAuthor = new Author() { Name = name };

                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/Authors", newAuthor);
                    return response;
                }
                catch (Exception) { throw; }
            }
        }

        public async Task<HttpResponseMessage> UpdateBook(Book model)
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync("api/Books", model);
                    return response;
                }
                catch (Exception) { throw; }
            }
        }

        public async Task<HttpResponseMessage> CreateNewBookWithAuthor(Author author, string bookName, int publicationYear = 2000)
        {
            var newBook = new Book()
            {
                AuthorId = author.Id,
                Name = bookName,
                // TODO Implement this properly
                PublicationYear = publicationYear
            };

            using (var client = new HttpClient())
            {
                InitializeClient(client);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/Books/AddBookWithAuthor", newBook);
                    return response;
                }
                catch (Exception) { throw; }
            }
        }
    }
}
