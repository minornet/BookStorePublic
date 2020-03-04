//using System.Web;
using System.Web.Mvc;
using BookStore.Filters;

namespace BookStore
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new OnExceptionAttribute());
            filters.Add(new BasicAuthenticationAttribute());
        }
    }
}
