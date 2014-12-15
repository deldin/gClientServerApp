using ApiServer.Filters;
using ApiServer.Helpers;
using ApiServer.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ApiServer.Controllers
{
    [HandleExceptionAttribute]
    public class TestController : BaseController
    {
        #region

        [HttpPut]
        [ActionName("RunTests")]
        public HttpResponseMessage Put()
        {
            try
            {
                string providerToUse = "NUnit";
                string errorsOrMessages = string.Empty;

                _clientModel.GetUserData(out _callerIP, out _callerName);
                var files = _clientModel.GetRecentUploads(_callerName, _callerIP);
                var file = files.OrderByDescending(z => z.Created).FirstOrDefault();

                if (file == null)
                    throw new FileNotFoundException("No recently uploaded files were found");

                var filePath = file.FilePath;
                var result = _commandModel.RunTests(providerToUse, filePath, out errorsOrMessages);

                //Log the test run results
                string logType = result ? "Info" : "Error";
                LogItem log = new LogItem()
                {
                    Id = Guid.NewGuid(),
                    Source = _callerIP,
                    LogText = errorsOrMessages,
                    LogType = logType,
                    Created = DateTime.Now
                };
                LogServerHelper logger = new LogServerHelper();
                logger.Post(log);

                return Request.CreateResponse<ExecutionResult>(HttpStatusCode.OK,
                    new ExecutionResult() { Value = result, ErrorsOrMessages = errorsOrMessages });
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        #endregion
    }
}