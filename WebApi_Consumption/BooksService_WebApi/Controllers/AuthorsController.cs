using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BooksService_WebApi.Controllers
{
    public class AuthorsController : ApiController
    {
        private IRepository<Book> bookRepository;
        private IRepository<Author> authorRepository;

        public AuthorsController(IRepository<Book> bookRepo, IRepository<Author> authorRepo)
        {
            bookRepository = bookRepo;
            authorRepository = authorRepo;
        }

        [HttpPost]
        public IHttpActionResult CreateAuthor(Author author)
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
                return Ok(authorRepository.Save() > 0);
                
            }

            return InternalServerError();
        }

        //[HttpPost]
        //public bool CreateAuthor(Author author)
        //{
        //    bool authorAlreadyExists = authorRepository.Find(x => x.Name.ToLower() == author.Name.ToLower()).Count() > 0;

        //    if (authorAlreadyExists)
        //    {
        //        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        //        {
        //            Content = new StringContent($"An author named '{author.Name}' already exists."),
        //            ReasonPhrase = $"An author named '{author.Name}' already exists."
        //        };

        //        throw new HttpResponseException(response);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        authorRepository.Add(author);
        //        return authorRepository.Save() > 0;
        //    }

        //    return false;
        //}

        [HttpGet]
        public List<Author> Get()
        {
            return authorRepository.GetAll().Include(x => x.Books).ToList();
        }
    }
}
