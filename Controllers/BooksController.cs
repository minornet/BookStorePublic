using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.DAL;
using BookStore.ViewModels;
using System.Web.ModelBinding;
using BookStore.Filters;
using BookStore.Services;
using System.Diagnostics;

namespace BookStore.Controllers
{
    [RoutePrefix("Book")]   // change url from Books to book
    public class BooksController : Controller
    {

        private BookService bookService;
        //private AuthorService authorService;

        BookViewModel bookViewModel = new BookViewModel();

        public BooksController()
        {
            bookService = new BookService();
            //authorService = new AuthorService();
        }

        // GET: Books
        [GenerateResultListFilterAttribute(typeof(Book), typeof(BookViewModel))]
        [Route("~/Books")] // Sets the url back to Books for the initial books list
        public ActionResult Books([Form] QueryOptions queryOptions)
        {
            var books = bookService.Get(queryOptions);
            ViewData["QueryOptions"] = queryOptions;
            return View(books);
        }

        // GET: Books/Details/5
        [Route("Details/{id:int:min(0)?}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = bookService.GetById(id.Value);
            return View(AutoMapper.Mapper.Map<Book, BookViewModel>(book));
        }

        // GET: Books/Create
        [BasicAuthorization]
        public ActionResult Create()
        {
            return View("BookForm", bookViewModel);
        }
        
        // GET: Books/Edit/5
        [BasicAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = bookService.GetById(id.Value); 

            return View("BookForm", AutoMapper.Mapper.Map<Book, BookViewModel>(book));
        }
        
        // GET: Books/Delete/5
        [BasicAuthorization]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = bookService.GetById(id.Value);
           
            return View(AutoMapper.Mapper.Map<Book, BookViewModel>(book));
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [BasicAuthorization]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = bookService.GetById(id);
            bookService.Delete(book);

            return RedirectToAction("Books");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bookService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
