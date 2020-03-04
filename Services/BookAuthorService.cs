using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.DAL;
using System.Data.Entity;
using BookStore.ViewModels;

// will either lose this or will need to create a new BookAuthor service to link books and authors


namespace BookStore.Services
{
    public class BookAuthorService : IDisposable
    {
        private BookStoreEntities db = new BookStoreEntities();
        
        // loading a list of BookAuthor records.
        // may be a smarter way of doing this using EF
        

        // ya, can't sort by author name or anything here
        public List<BookAuthor> GetBookAuthorByBookId(int id)
        {
            var xRef = new List<BookAuthor>();

            foreach (var item in db.BookAuthor
                .Where(x => x.BookId == id))
            {
                xRef.Add(item);
            }
            
          //  Author author = db.Authors
          //.Where(a => a.FirstName + ' ' + a.LastName == name)
          //.SingleOrDefault();
          
            if (xRef == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException(string.Format("Unable to find authors with book id {0}", id));
            }

            return xRef;
        }

        //public void Insert(BookAuthor bookAuthor)
        //{
        //    db.BookAuthor.Add(bookAuthor);
        //    db.SaveChanges();
        //}

        //public void Update(Author author)
        //{
        //    db.Entry(author).State = EntityState.Modified;
        //    db.SaveChanges();
        //}

        //public void Delete(BookAuthor bookAuthor)
        //{
        //    db.BookAuthor.Remove(bookAuthor);
        //    db.SaveChanges();
        //}

        public void Dispose()
        {
            db.Dispose();
        }

    }
}