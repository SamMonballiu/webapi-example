using BooksService_WebApi.Models;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using DataLayer;

namespace BooksService_WebApi.Controllers
{
    public class BooksController : ApiController
    {
        private IRepository<Book> bookRepository;
        private IRepository<Author> authorRepository;

        public BooksController(IRepository<Book> bookRepo, IRepository<Author> authorRepo)
        {
            bookRepository = bookRepo;
            authorRepository = authorRepo;
        }

        [HttpGet]
        public IEnumerable<Book> Get() => bookRepository.GetAll();

        [HttpGet]
        public Book GetBooks(int id) => bookRepository.Get(id);

        
        [HttpPost]
        public bool AddBookWithAuthor(Book model)
        {
            var author = model.Author ?? authorRepository.GetAll().Include(x => x.Books).FirstOrDefault(a => a.Id == model.AuthorId);
            if (author.Books.Any(book => book.Name.ToLower() == model.Name.ToLower()))
            {
                string bookAlreadyExistsForAuthor = $"A book named '{model.Name}' already exists for author {author.Name}";
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(bookAlreadyExistsForAuthor),
                    ReasonPhrase = bookAlreadyExistsForAuthor
                };

                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                bookRepository.Add(model);
                return bookRepository.Save() > 0;
            }

            return false;
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var book = GetBooks(id);
            if (book is null)
            {
                return false;
            }

            else
            {
                bookRepository.Remove(book);
                return bookRepository.Save() > 0;
            }
        }
    }
}
