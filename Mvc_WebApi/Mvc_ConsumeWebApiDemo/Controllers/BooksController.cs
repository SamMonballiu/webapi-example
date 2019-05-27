using Mvc_ConsumeWebApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}