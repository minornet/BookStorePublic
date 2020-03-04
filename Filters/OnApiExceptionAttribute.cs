using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using BookStore.ViewModels;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace BookStore.Filters
{
    public class OnApiExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exceptionType = actionExecutedContext.Exception.GetType().Name;

            ReturnData returnData;

            switch (exceptionType)
            {
                case "ObjectNotFoundException":
                    returnData = new ReturnData(HttpStatusCode.NotFound, actionExecutedContext.Exception.Message, "Error");
                    break;

                default:  // if exception is not handled, dump the stack trace
                    string TraceString = GetAllFootprints(actionExecutedContext);
                    // returnData.content not displaying, but should be
                    returnData = new ReturnData(HttpStatusCode.InternalServerError, exceptionType + " : " +
                        "An error occured, please try again or contact the administrator.<br /><br />",
                        //      "Error");
                        TraceString);  // this IS displaying but we really want to log it instead.
                    break;
            }

            string content = returnData.Content;
            string reasonPhrase = returnData.ReasonPhrase;

            actionExecutedContext.Response = new HttpResponseMessage(returnData.HttpStatusCode)
            {
                Content = new StringContent(content),
                ReasonPhrase = reasonPhrase
            };
        }

        // found this handy bit on stack exchange
        // is really designed for exceptions, but we can extract the exception from the context

        public string GetAllFootprints(HttpActionExecutedContext x)
        {
            var st = new StackTrace(x.Exception, true);
            var frames = st.GetFrames();
            var traceString = new StringBuilder();

            foreach (var frame in frames)
            {
                if (frame.GetFileLineNumber() < 1)
                    continue;

                traceString.Append("File: " + frame.GetFileName());
                traceString.Append(", Method:" + frame.GetMethod().Name);
                traceString.Append(", LineNumber: " + frame.GetFileLineNumber());
                traceString.Append("  <br />-->  ");
            }

            return traceString.ToString();
        }

    }
}