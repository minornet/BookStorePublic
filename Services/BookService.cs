using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using BookStore.DAL;
using System.Data.Entity;
using BookStore.ViewModels;
using BookStore.Behaviors;

namespace BookStore.Services
{
    public class BookService : IDisposable
    {
        private BookStoreEntities db = new BookStoreEntities();

        public List<Book> Get(QueryOptions queryOptions)
        {
            // First time will be Id
            if (queryOptions.SortField == "Id")
            {
                queryOptions.SortField = "Title";
            }

            var start = QueryOptionsCalculator.CalculateStart(queryOptions);

            var books = db.Book.
                OrderBy(queryOptions.Sort).
                Skip(start).
                Take(queryOptions.PageSize);

            queryOptions.TotalPages =
                QueryOptionsCalculator.CalculateTotalPages(db.Book.Count(), queryOptions.PageSize);
            
            return books.ToList();

        }

        public Book GetById(long id)
        {
            Book book = db.Book.Find(id);

            if (book == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException(string.Format("Unable to find book with id {0}", id));
            }

            return book;
        }

        public Book GetByTitle(string title)
        {
            Book book = db.Book
                .Where(a => a.Title == title)
                .SingleOrDefault();

            if (title == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException(string.Format("Unable to find book with title {0}", title));
            }

            return book;
        }

        public void Insert(Book book)
        {
            db.Book.Add(book);
            db.SaveChanges();
        }

        public void Update(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Book book)
        {

            // lose the bookauthors first
            var bookAuthors = from ba in db.BookAuthor
                              where ba.BookId.Equals(book.Id)
                              select ba;

            foreach (var item in bookAuthors)
            {
                db.BookAuthor.Remove(item);
            }

            db.Book.Remove(book);
            db.SaveChanges();

        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}