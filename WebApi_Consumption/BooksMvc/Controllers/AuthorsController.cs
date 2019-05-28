using BooksMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksMvc.Controllers
{
    public class AuthorsController : BaseController
    {
        // GET: Authors
        [Route("/authors/{id}")]
        public ActionResult Index(int? id)
        {
            var authors = bookService.GetAuthors().Result;
            var books = bookService.GetBooks().Result;

            var vm = new AuthorsViewModel()
            {
                Authors = authors,
                SelectedBook = id == null ? null : books.Find(x => x.Id == id)
            };

            if (vm.SelectedBook != null)
            {
                vm.SelectedBook.Author = authors.Find(x => x.Id == vm.SelectedBook.AuthorId); 
            }

            return View(vm);
        }
    }
}