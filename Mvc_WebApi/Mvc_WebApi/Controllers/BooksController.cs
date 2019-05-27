using Mvc_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mvc_WebApi.Controllers
{
    public class BooksController : ApiController
    {
        

        private static List<Book> books = new List<Book>()
        {
            new Book {
                Id = 1,
                Author = "Harper Lee",
                Title = "To Kill A Mockingbird",
                PublicationYear = 1960,
                CallNumber = "HLMockingbird",
                IsAvailable = true
            },

            new Book
            {
                Id = 2,
                Author = "Jonathan Safran Foer",
                Title = "Extremely Loud and Incredibly Close",
                PublicationYear = 2005,
                CallNumber = "JSFoerExtremelyLoud",
                IsAvailable = true
            },

            new Book
            {
                Id = 3,
                Author = "John Steinbeck",
                Title = "The Grapes of Wrath",
                PublicationYear = 1939,
                CallNumber = "JSGrapesOfWrath",
                IsAvailable = true
            }
        };

        #region GET methods

        // return all books
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return books;
        }

        // search books by id
        // GET api/books/5
        [HttpGet]
        public Book GetBook(int id)
        {
            return books.Find(book => book.Id == id);
        }

        [HttpGet]
        [Route("Api/Books/GetByAuthor/{author}")]
        // GET api/books/GetByAuthor/hosseini
        public Book Get(string author)
        {
            return books.Find(book => book.Author.ToLower().Contains(author.ToLower()));
        }

        #endregion

        #region DELETE method

        [HttpDelete]
        public bool Remove(int id)
        {
            // find the book with the specified id
            var book = GetBook(id);

            // if book does not exist, return false
            if (book == null)
            {
                return false;
            }

            // if book does exist, remove it and return true
            books.Remove(book);
            return true;
        }
        #endregion


        #region POST method

        [HttpPost]
        //[Route("api/Books/AddNewBook")]
        public bool AddNewBook(Book book)
        {
            books.Add(book);
            return true;
        }

        #endregion

        [HttpPut]
        public List<Book> UpdateBook(int id, Book book)
        {
            //try to remove the book with the id from the list.
            // If it succeeds, add the new book to the list in its place.
            if (Remove(id) == true)
            {
                AddNewBook(book);
            }

            // return the list of books.
            return books;
        }
    }
}
