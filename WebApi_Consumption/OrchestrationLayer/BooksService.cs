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
    public static class BooksService
    {
        public static async Task<HttpResponseMessage> AddAuthor(string name)
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
                    return response;
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    return true;
                    //}

                    //else
                    //{
                    //    return false;
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }

            }

        }
    }
}
