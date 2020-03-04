using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.DAL;

namespace BookStore.ViewModels {

    public class AuthorList
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Biography { get; set; }

        // get a generic list of all the authors (not used?)
        public List<AuthorViewModel> GetListOfAuthors()
        {

            var context = new BookStoreEntities();
            var result = from a in context.Author
                         select new AuthorList()
                         {
                             Id = a.Id,
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                             Biography = a.Biography
                         };

            var AuthorsList = new List<AuthorViewModel>();

            foreach (var item in result)
            {
                var author = new AuthorViewModel();
                author.Id = item.Id;
                author.FirstName = item.FirstName;
                author.LastName = item.LastName;
                author.Biography = item.Biography;
                AuthorsList.Add(author);
            }

            return AuthorsList;
        }

        // get a generic list of authors for one book
        public List<AuthorViewModel> GetListOfAuthors(int BookId)
        {
            var AuthorsList = new List<AuthorViewModel>();

            var context = new BookStoreEntities();
            var result = from a in context.Author
                         join r in context.BookAuthor
                         on a.Id equals r.AuthorId
                         where r.BookId == BookId
                         orderby(a.LastName + " " + a.FirstName)
                         select new AuthorList()
                         {
                             Id = a.Id,
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                             Biography = a.Biography
                         };

            foreach (var item in result)
            {
                var author = new AuthorViewModel();
                author.Id = item.Id;
                author.FirstName = item.FirstName;
                author.LastName = item.LastName;
                author.Biography = item.Biography;
                AuthorsList.Add(author);
            }

            return AuthorsList;
        }

        // get an IEnumerable list of authors for one book, 
        // maybe drive us to change the view model back to IEnumerable

        public IEnumerable<AuthorViewModel> GetAuthors(int BookId)
        {
            var AuthorsList = new List<AuthorViewModel>();

            var context = new BookStoreEntities();
            var result = from a in context.Author
                         join r in context.BookAuthor
                         on a.Id equals r.AuthorId
                         where r.BookId == BookId
                         select new AuthorList()
                         {
                             Id = a.Id,
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                             Biography = a.Biography
                         };

            // copy the IQueryable to the List<AuthorViewModel>
            // would be nice if we didn't have to do this.

            foreach (var item in result)
            {
                var author = new AuthorViewModel();
                author.Id = item.Id;
                author.FirstName = item.FirstName;
                author.LastName = item.LastName;
                author.Biography = item.Biography;
                AuthorsList.Add(author);
            }

            return AuthorsList;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}