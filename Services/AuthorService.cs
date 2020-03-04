using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.DAL;
//using BookStore.Models;
using System.Data.Entity;
using BookStore.ViewModels;
using BookStore.Behaviors;

namespace BookStore.Services
{
    public class AuthorService : IDisposable
    {
        private BookStoreEntities db = new BookStoreEntities();

        public List<Author> Get(QueryOptions queryOptions, int bookId = 0)
        {
            // First time will be Id
            if (queryOptions.SortField == "Id")
            {
                queryOptions.SortField = "LastName";
            }

            var start = QueryOptionsCalculator.CalculateStart(queryOptions);

            if (bookId == 0)
            {
                var authors = db.Author.
                    OrderBy(queryOptions.Sort).
                    Skip(start).
                    Take(queryOptions.PageSize);

                queryOptions.TotalPages =
                QueryOptionsCalculator.CalculateTotalPages(db.Author.Count(), queryOptions.PageSize);

                return authors.ToList();
            }
            else
            {   
                var authors = (from a in db.Author
                               join b in db.BookAuthor
                               on a.Id equals
                                  b.AuthorId
                               where b.BookId == bookId
                               select a).
                    OrderBy(queryOptions.Sort);

                queryOptions.TotalPages = 1;

                return authors.ToList();
            }

        }

        public Author GetById(int id)
        {
            Author author = db.Author.Find(id);
            if (author == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException(string.Format("Unable to find author with id {0}", id));
            }

            return author; 
        }

        public Author GetByName(string name)
        {
            Author author = db.Author
                .Where(a => a.FirstName + ' ' + a.LastName == name)
                .SingleOrDefault();

            if (author == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException(string.Format("Unable to find author with name {0}", name));
            }

            return author;
        }

        public void Insert(Author author)
        {
            db.Author.Add(author);
            db.SaveChanges();
        }

        public void Update(Author author)
        {
            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Author author)
        {
            // lose the bookauthors first
            var bookAuthors = from ba in db.BookAuthor
                              where ba.AuthorId.Equals(author.Id)
                              select ba;

            foreach (var item in bookAuthors)
            {
                db.BookAuthor.Remove(item);
            }

            db.Author.Remove(author);
            db.SaveChanges();
        }
        
        public void RemoveBookAuthor(BookAuthor bookAuthor)
        {
            db.BookAuthor.Remove(bookAuthor);
            db.SaveChanges();
        }

        public void AddBookAuthor(BookAuthor bookAuthor)
        {
            db.BookAuthor.Add(bookAuthor);
            db.SaveChanges();
        }

        public BookAuthor GetBookAuthor(int bookId, int authorId)
        {
            BookAuthor bookAuthor = db.BookAuthor
            .Where(a => a.BookId == bookId && a.AuthorId == authorId)
            .SingleOrDefault();

            return bookAuthor;
        }
        
        public void Dispose()
        {
            db.Dispose();
        }

    }
}