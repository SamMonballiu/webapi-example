using BooksMvc.Models;
using DataLayer.Models;
using System.Linq;
using System.Threading.Tasks;
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