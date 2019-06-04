using BooksMvc.Models;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BooksMvc.Controllers
{
    public class AuthorsController : BaseController
    {
        // GET: Authors
        [Route("/authors/{id}")]
        public ActionResult Index(int? id, int page = 1)
        {
            var authors = bookService.GetAuthors().Result;
            var books = bookService.GetBooks().Result;
            int itemsPerPage = 10;

            ViewBag.PageCount = Math.Ceiling(authors.Count() / (double)itemsPerPage);
            if (page < 0)
            {
                page = 0;
            }

            if (page > ViewBag.PageCount)
            {
                page = (int)ViewBag.PageCount;
            }

            ViewBag.CurrentPage = page;
            int start = (page - 1) * itemsPerPage;

            //for (int i = authors.Count; i < 150; i++)
            //{
            //    var aut = new Author()
            //    {
            //        Name = $"Example Author #{i.ToString()}",
            //        Books = new List<Book>()
            //    };
            //    authors.Add(aut);
            //}

            var vm = new AuthorsViewModel()
            {
                Authors = authors.Skip(start).Take(itemsPerPage).ToList(),
                SelectedBook = id == null ? null : books.Find(x => x.Id == id)
            };

            if (vm.SelectedBook != null)
            {
                vm.SelectedBook.Author = authors.Find(x => x.Id == vm.SelectedBook.AuthorId);
            }

            return View(vm);
        }

        public ActionResult Details(int id)
        {
            var author = bookService.GetAuthor(id).Result;
            author.Books = (bookService.GetBooks().Result).Where(bk => bk.AuthorId == author.Id).ToList();
            if (author != null)
            {
                return PartialView("_AuthorDetails", author);
            }

            return View("Error");
        }

        public ActionResult Create()
        {
            return PartialView("_CreateAuthor");
        }

        [HttpPost]
        public async Task<ActionResult> Create(Author model)
        {
            await bookService.CreateAuthor(model.Name);
            return RedirectToAction("Index");
        }
    }
}