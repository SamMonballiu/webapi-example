using OrchestrationLayer;
using System.Web.Mvc;

namespace BooksMvc.Controllers
{
    public class BaseController : Controller
    {
        public BooksService bookService { get; } = new BooksService("http://localhost:51518/");
    }
}