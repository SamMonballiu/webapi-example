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
                    var books = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Book>>(await response.Content.ReadAsStringAsync());
                    return books;
                }
                catch (JsonSerializationException) { return null; }
                catch (Exception) { throw; }
            }
        }

        public async Task<HttpResponseMessage> AddAuthor(string name)
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

        public async Task<HttpResponseMessage> CreateNewBookWithAuthor(Author author, string bookName)
        {
            var newBook = new Book()
            {
                AuthorId = author.Id,
                Name = bookName,
                // TODO Implement this properly
                PublicationYear = 2000
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
