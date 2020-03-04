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
    public class AuthorsController : ApiController
    {

        private AuthorService authorService;
        
        public AuthorsController()
        {
            authorService = new AuthorService();

            // AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();  
            // AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();  

        }


        //// GET api/<controller>                  // want to test this, make sure it works right
        //[ResponseType(typeof(AuthorViewModel))]
        //public IHttpActionResult Get(int id)
        //{
        //    Author author = db.Authors.Find(id);
        //    if (author == null)
        //    {
        //        throw new System.Data.Entity.Core.ObjectNotFoundException
        //            (string.Format("Unable to find author with id {0}", id));
        //    }

        //    return Ok(AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        //}
                
        // GET: api/Authors
        public ResultList<AuthorViewModel> Get([FromUri]QueryOptions queryOptions)
        {

            var authors = authorService.Get(queryOptions);

            //var start = (queryOptions.CurPage - 1) * queryOptions.PageSize;

            //var authors = db.Authors
            //    .OrderBy(queryOptions.Sort)
            //    .Skip(start)
            //    .Take(queryOptions.PageSize);

            //queryOptions.TotalPages = (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);

            // AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();  // old version
            // return new ResultList<AuthorViewModel>;

            var response = new ResultList<AuthorViewModel>(AutoMapper.Mapper.Map<List<Author>,
                List<AuthorViewModel>>(authors), queryOptions);
            // List<AuthorViewModel>>(authors.ToList()), queryOptions);

            return response;

            //{
            //    QueryOptions = queryOptions,
            //    Results = AutoMapper.Mapper.Map<List<Author>, List<AuthorViewModel>>(authors.ToList())
            //};

        }

        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(AuthorViewModel author)
        {

            var model = AutoMapper.Mapper.Map<AuthorViewModel,Author>(author);

            authorService.Update(model);
            
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //// AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();

            //MappingConfig.RegisterMaps();  // this is static so may not need. also maybe static is why kaboom since this time we're trying to change the record?
            
            //db.Entry(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author)).State = EntityState.Modified;

            //db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
        

        // POST: api/Authors
        [ResponseType(typeof(AuthorViewModel))]
        public IHttpActionResult Post(AuthorViewModel author) {

            var model = AutoMapper.Mapper.Map<AuthorViewModel, Author>(author);

            authorService.Insert(model);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //// AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
            //db.Authors.Add(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author));
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            var author = authorService.GetById(id);

            authorService.Delete(author);

            return Ok(author);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                authorService.Dispose();
            }
            base.Dispose(disposing);
        }

        /*
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        */
    }
}