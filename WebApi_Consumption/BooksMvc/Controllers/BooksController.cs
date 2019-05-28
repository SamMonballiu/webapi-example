using BooksMvc.Models;
using DataLayer.Models;
using OrchestrationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BooksMvc.Controllers
{
    public class BooksController : BaseController
    {
        // GET: Books
        public ActionResult Index(int? id)
        {
            var books = bookService.GetBooks().Result;
            var authors = bookService.GetAuthors().Result;

            foreach (var book in books)
            {
                book.Author = authors.Find(x => x.Id == book.AuthorId);
            }

            var vm = new BooksViewModel()
            {
                Books = books,
                SelectedAuthor = id == null ? null : authors.Find(x => x.Id == id)
            };

            return View(vm);
        }

    }
}