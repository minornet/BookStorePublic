
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using BookStore.DAL;
//using AutoMapper;
using BookStore.App_Start;

namespace BookStore
{

    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            {
                // Default stuff
                AreaRegistration.RegisterAllAreas();

                // Manually installed WebAPI 2.2 after making an MVC project.
                GlobalConfiguration.Configure(WebApiConfig.Register); // NEW way
                //WebApiConfig.Register(GlobalConfiguration.Configuration); // DEPRECATED

                // Default stuff
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);

                // GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
                // GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

                MappingConfig.RegisterMaps();

                //AutoMapper.Mapper.Initialize(cfg => {
                //    cfg.CreateMap<Author, AuthorViewModel>();
                //});

                // going to database first
                //var bookContext = new BookContext();
                //Database.SetInitializer(new BookInitializer());

                // try catch if database not available (ethernet was unplugged, gave exception)
                // bookContext.Database.Initialize(true);

                




            }

        }

    }

}
    