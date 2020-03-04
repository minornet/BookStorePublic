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


namespace BookStore.Controllers
{
    [RoutePrefix("Author")]   // this changes the url from Authors to Author
    public class AuthorsController : Controller
    {

        private AuthorService authorService; 
        
        public AuthorsController()
        {
            authorService = new AuthorService();
        }


        // GET: Authors
        [GenerateResultListFilterAttribute(typeof(Author), typeof(AuthorViewModel))]
        [Route("~/Authors")] // Sets the url back to Authors for the initial authors list
        public ActionResult Authors([Form] QueryOptions queryOptions)         // ko version replaces Index
        {
            var authors = authorService.Get(queryOptions);
            ViewData["QueryOptions"] = queryOptions;
            return View(authors);
        }


        // GET: BookAuthors
        [GenerateResultListFilterAttribute(typeof(Author), typeof(AuthorViewModel))]
        [Route("~/BookAuthors")] // Sets the url back to BookAuthors for the BookAuthors list
        public ActionResult BookAuthors([Form] QueryOptions queryOptions, int bookId, string bookTitle)  
        {
            var authors = authorService.Get(queryOptions, bookId);
            ViewData["QueryOptions"] = queryOptions;
            ViewData["BookId"] = bookId;
            ViewData["BookTitle"] = bookTitle;
            return View(authors);
        }


        // POST: Authors/AddBookAuthor/5,5
        //[HttpPost, ActionName("AddBookAuthor")]
        //[ValidateAntiForgeryToken]
        [BasicAuthorization]
        public ActionResult AddBookAuthor(int bookId, int authorId)
        {
            var bookAuthor = new BookAuthor();
            bookAuthor.BookId = bookId;
            bookAuthor.AuthorId = authorId;
            authorService.AddBookAuthor(bookAuthor);
            return RedirectToAction("BookAuthors", new { bookId });
        }


        // POST: Authors/RemoveBookAuthor/5,5
        //[HttpPost, ActionName("RemoveBookAuthor")]
        //[ValidateAntiForgeryToken]
        [BasicAuthorization]
        public ActionResult RemoveBookAuthor(int bookId, int authorId)
        {
            var bookAuthor = authorService.GetBookAuthor(bookId, authorId);
            authorService.RemoveBookAuthor(bookAuthor);
            return RedirectToAction("BookAuthors", new { bookId });
        }


        // GET: Authors/Details/5
        [Route("Details/{id:int:min(0)?}")]
        public ActionResult Details(int? id)         // is this right?  do we need a getbyname?
        
        // public ActionResult GetById(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = authorService.GetById(id.Value);

            //  details view is not set up to use ViewModel yet.
            //  return View(AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));

            return View(author);  // this works
        }
        

        // GET: Authors/Create
        [BasicAuthorization]
        public ActionResult Create()
        {
            return View("AuthorForm", new AuthorViewModel());
        }
        

        // GET: Authors/Edit/5
        [BasicAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = authorService.GetById(id.Value);
            if (author == null)
            {
                return HttpNotFound();
            }

            return View("AuthorForm", AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        }

        /*

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Biography")] AuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
                db.Entry(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author)).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Authors");
            }
            return View(author);
        }

        */

        // GET: Authors/Delete/5
        [BasicAuthorization]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = authorService.GetById(id.Value);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [BasicAuthorization]
        public ActionResult DeleteConfirmed(int id)
        {
            var author = authorService.GetById(id);
            authorService.Delete(author);

            return RedirectToAction("Authors");
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                authorService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
