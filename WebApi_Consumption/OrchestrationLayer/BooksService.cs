using DataLayer.Models;
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

        public async Task<List<Author>> GetAuthors()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/Authors");
                    var authors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Author>>(await response.Content.ReadAsStringAsync());
                    return authors;
                }
                catch (Exception) { throw; }
            }
        }

        public async Task<HttpResponseMessage> AddAuthor(string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
                PublicationYear = 2000
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
