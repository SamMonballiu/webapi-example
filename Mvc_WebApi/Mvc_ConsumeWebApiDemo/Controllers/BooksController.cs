using Mvc_ConsumeWebApiDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mvc_ConsumeWebApiDemo.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBooks()
        {
            IEnumerable<Book> books = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50070/api/books"); //show the client the way to the WebApi service.
                var response = client.GetAsync("books");
                response.Wait(); // wait for the client.GetAsync("books") to be completed.

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsStringAsync();
                    books = JsonConvert.DeserializeObject<IEnumerable<Book>>(read.Result);
                }

                else
                {
                    books = Enumerable.Empty<Book>();
                    ModelState.AddModelError(String.Empty, "Server error, please try again later.");
                }
            }
            return View(books);
        }
    }
}