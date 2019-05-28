﻿using BooksMvc.Models;
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

        public ActionResult Edit(int id)
        {
            var book = bookService.GetBook(id).Result;
            var authors = bookService.GetAuthors().Result;
            var viewModel = new EditBookViewModel()
            {
                AvailableAuthors = new SelectList(authors, "Id", "Name"),
                SelectedBook = book
            };

            if (viewModel.SelectedBook != null)
            {
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(EditBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newAuthor = bookService.GetAuthor(model.SelectedAuthorId).Result;
                model.SelectedBook.Author = newAuthor;
                bookService.UpdateBook(model.SelectedBook).Wait();

                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}