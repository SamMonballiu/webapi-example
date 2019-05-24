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
        public Book Get(int id) => bookRepository.Get(id);

        [HttpPost, Route("api/Books/CreateAuthor")]
        public bool CreateAuthor(Author author)
        {
            bool authorAlreadyExists = authorRepository.Find(x => x.Name.ToLower() == author.Name.ToLower()).Count() > 0;

            if (authorAlreadyExists)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"An author named '{author.Name}' already exists."),
                    ReasonPhrase = $"An author named '{author.Name}' already exists."
                };

                throw new HttpResponseException(response);
            }

            if (ModelState.IsValid)
            {
                authorRepository.Add(author);
                return authorRepository.Save() > 0;
            }

            return false;
        }


        [HttpPost]
        public bool AddBookWithAuthor(Book model)
        {
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
            var book = Get(id);
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
