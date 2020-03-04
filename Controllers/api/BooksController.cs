using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookStore.DAL;
//using BookStore.Models;
using BookStore.ViewModels;
using BookStore.App_Start;
using Newtonsoft.Json;
using BookStore.Services;

namespace BookStore.Controllers.Api

{
    public class BooksController : ApiController
    {

        private BookService bookService;
        
        public BooksController()
        {
            bookService = new BookService();

        }
                
        // GET: api/Books
        public ResultList<BookViewModel> Get([FromUri]QueryOptions queryOptions)
        {

            var books = bookService.Get(queryOptions);

            var response = new ResultList<BookViewModel>(AutoMapper.Mapper.Map<List<Book>,
                List<BookViewModel>>(books), queryOptions);

            return response;

        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(BookViewModel book)
        {

            var model = AutoMapper.Mapper.Map<BookViewModel,Book>(book);

            bookService.Update(model);
            
            return StatusCode(HttpStatusCode.NoContent);
        }
        

        // POST: api/Books
        [ResponseType(typeof(BookViewModel))]
        public IHttpActionResult Post(BookViewModel book) {

            var model = AutoMapper.Mapper.Map<BookViewModel, Book>(book);

            bookService.Insert(model);

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            var book = bookService.GetById(id);

            bookService.Delete(book);

            return Ok(book);
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