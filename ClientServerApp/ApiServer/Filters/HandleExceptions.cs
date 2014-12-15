using ApiServer.Helpers;
using Entities;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace ApiServer.Filters
{
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext context)
        {
            try
            {                
                LogServerHelper logger = new LogServerHelper();
                logger.Post(new LogItem() { Id = Guid.NewGuid(), LogType = "Error", Source = "ApiServer", LogText = context.Exception.StackTrace });
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("ApiServer", context.Exception.StackTrace);
                EventLog.WriteEntry("ApiServer", ex.Message);
            }
            throw new HttpResponseException(
            new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An error occurred!"),
                ReasonPhrase = "Please check Log server for more details."
            });
        }
    }

}