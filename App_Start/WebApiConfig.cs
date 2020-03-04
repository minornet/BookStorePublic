//using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using BookStore.Filters;

namespace BookStore
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            config.Filters.Add(new ValidationActionFilterAttribute());
            config.Filters.Add(new OnApiExceptionAttribute());
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                // defaults: new { controller = "Authors", id = RouteParameter.Optional }
                defaults: new {  id = RouteParameter.Optional }
                );
            
            // finally fixed the ajax call return type
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();
        }
    }
}