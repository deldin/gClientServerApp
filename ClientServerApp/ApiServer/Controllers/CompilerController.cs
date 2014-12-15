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
    public class CompilerController : BaseController
    {
        #region

        [HttpPut]
        [ActionName("Compile")]
        public HttpResponseMessage Put()
        {
            try
            {
                string compilerToUse = "Compiler";
                string errorsOrMessages = string.Empty;

                _clientModel.GetUserData(out _callerIP, out _callerName);
                var files = _clientModel.GetRecentUploads(_callerName, _callerIP);
                var file = files.OrderByDescending(z => z.Created).FirstOrDefault();

                if (file == null)
                    throw new FileNotFoundException("No recently uploaded files were found");

                var filePath = file.FilePath;
                if (Path.GetExtension(file.FileName).ToLower() == ".zip")
                {
                    var extractPath = Path.GetFullPath(file.FilePath);
                    _fileModel.UnZip(file.FilePath, Path.GetFullPath(file.FilePath));
                    file.FilePath = extractPath;
                    compilerToUse = "MSBuild";
                }

                var result = _commandModel.Compile(compilerToUse, filePath, out errorsOrMessages);

                //Log the test compilation results
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