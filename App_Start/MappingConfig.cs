//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using BookStore.DAL;
using BookStore.ViewModels;

namespace BookStore.App_Start
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Author, AuthorViewModel>().ReverseMap();
                config.CreateMap<Book, BookViewModel>().ReverseMap();
            });
        }
    }
}