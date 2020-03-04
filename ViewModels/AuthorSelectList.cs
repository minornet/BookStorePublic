using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using BookStore.DAL;

namespace BookStore.ViewModels
{

    public class AuthorSelectList
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // need to remove authors from list that are already in the book
        public SelectList GetAuthorUnSelectList(int BookId)
        {
            var context = new BookStoreEntities();

            // performs a left join on book Id and leaves out authors already assigned to the book.
            // this isn't perfect yet.  If we have two different books by the same author, then it will still
            // show up in the add list (just be careful for now).

            var result = (
                from a in context.Author
                join r in context.BookAuthor
                    on a.Id equals r.AuthorId into ar
                from ba in ar.DefaultIfEmpty()
                where (ba.BookId != BookId)
                select new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.LastName + ", " + a.FirstName
                })
                .Distinct()
                .OrderBy(x => x.Text);

            var authorSelectList = new SelectList(
               result, "Value", "Text");

            return authorSelectList;
           
        }

        public SelectList GetAuthorSelectList(int Bookid)
        {
            var context = new BookStoreEntities();
            var result = from a in context.Author
                         join r in context.BookAuthor
                         on a.Id equals r.AuthorId
                         where r.BookId == Bookid
                         orderby FullName                           // not sure this works here (see above)
                         select new AuthorSelectList()
                         {
                             Id = a.Id,
                             FullName = a.LastName + ", " + a.FirstName
                         };

            var selectListItems = new List<SelectListItem>();

            foreach (var item in result)
            {
                var selectListItem = new SelectListItem
                {
                    Text = item.FullName,
                    Value = item.Id.ToString()
                };

                selectListItems.Add(selectListItem);
            }

            var authorSelectList = new SelectList(
                selectListItems, "Value", "Text");

            return authorSelectList;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }

}