using BookStore.ViewModels;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;


public static class HtmlHelperExtensions
{
    
    public static MvcHtmlString BuildKnockoutNextPreviousLinks(
        this HtmlHelper htmlHelper, string actionName)
    {
        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

        MvcHtmlString mvcHtmlString = new MvcHtmlString(string.Format(
            "<nav>" +
            "    <ul class=\"pager\">" +
            "       <li data-bind=\"css: pagingService.buildPreviousClass()\">" +
            "        <a href=\"{0}\" data-bind=\"click: pagingService.previousPage\">" +
            "           Previous</a></li>" +
            "       <li data-bind=\"css: pagingService.buildNextClass()\">" +
            "           <a href=\"{0}\" data-bind=\"click: pagingService.nextPage\">Next" +
            "        </a></li></li>" +
            "    </ul>" +
            "</nav>",
            @urlHelper.Action(actionName)
            ));

        return mvcHtmlString;

    }

    private static string IsPreviousDisabled(QueryOptions queryOptions)
    {
        return (queryOptions.CurPage == 1) ? "disabled" : string.Empty;
    }

    private static string IsNextDisabled(QueryOptions queryOptions)
    {
        return (queryOptions.CurPage == queryOptions.TotalPages) ? "disabled" : string.Empty;
    }


    public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        return new HtmlString(JsonConvert.SerializeObject(model, settings));
    }


    public static MvcHtmlString BuildKnockoutSortableLink(this HtmlHelper htmlHelper,
        string fieldName, string actionName, string sortField)

    {
        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

        MvcHtmlString KSLink =
            new MvcHtmlString(string.Format(
                "<a href=\"{0}\" data-bind=\"click: pagingService.sortEntitiesBy\"" +
                " data-sort-field=\"{1}\">{2} " +
                "<span data-bind=\"css: pagingService.buildSortIcon('{1}')\"></span></a>",

                urlHelper.Action(actionName),
                sortField,
                fieldName));
        
        return KSLink;

    }

}
