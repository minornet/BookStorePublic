﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.Mvc;
using System.Net;
using BookStore.ViewModels;

namespace BookStore.Filters
{
    public class OnExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            var exceptionType = exceptionContext.Exception.GetType().Name;

            ReturnData returnData;

            switch (exceptionType)
            {
                case "ObjectNotFoundException":
                    returnData = new ReturnData(HttpStatusCode.NotFound, exceptionContext.Exception.Message, "Error");
                    break;

                default:
                    returnData = new ReturnData(HttpStatusCode.InternalServerError, "An error occurred, please try again or contact the administrator.", "Error");
                    break;
            }

            exceptionContext.Controller.ViewData.Model = returnData.Content;
            exceptionContext.HttpContext.Response.StatusCode = (int)returnData.HttpStatusCode;
            exceptionContext.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = exceptionContext.Controller.ViewData
            };

            exceptionContext.ExceptionHandled = true;

            base.OnException(exceptionContext);

        }
    }
}