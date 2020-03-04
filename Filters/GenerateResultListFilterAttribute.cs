using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;
using BookStore.ViewModels;
using System.Web.Mvc;
using System.ComponentModel;


namespace BookStore.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GenerateResultListFilterAttribute : FilterAttribute, IResultFilter
    {
        private readonly Type _sourceType;
        private readonly Type _destinationType;

        public GenerateResultListFilterAttribute(Type sourceType, Type destinationType)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
            
            // Dynamic AutoMapper again  // may not need this, same as it ever was.  fixed w/reverse
            //AutoMapper.Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap(_sourceType, _destinationType);
            //});

        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;

            var resultListGenericType = typeof(ResultList<>).MakeGenericType(new Type[] { _destinationType });
            var srcGenericType = typeof(List<>).MakeGenericType(new Type[] { _sourceType });
            var destGenericType = typeof(List<>).MakeGenericType(new Type[] { _destinationType });

   //         AutoMapper.Mapper.CreateMap(_sourceType, _destinationType);
            var viewModel = AutoMapper.Mapper.Map(model, srcGenericType, destGenericType);

            var queryOptions = filterContext.Controller.ViewData.ContainsKey("QueryOptions") ? filterContext.Controller.ViewData["QueryOptions"] : new QueryOptions();

            var resultList = Activator.CreateInstance(resultListGenericType, viewModel, queryOptions);

            filterContext.Controller.ViewData.Model = resultList;

        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
    }
}