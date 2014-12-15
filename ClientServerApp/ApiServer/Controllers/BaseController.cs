using ApiServer.Filters;
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
    public abstract class BaseController : ApiController
    {
        protected string _callerIP;
        protected string _callerName;
        protected CommandModel _commandModel = new CommandModel();
        protected ClientModel _clientModel = new ClientModel();
        protected FileModel _fileModel = new FileModel();

        #region

        [HttpGet]
        [ActionName("Connect")]
        public HttpResponseMessage Get()
        {
            try
            {
                _clientModel.GetUserData(out _callerIP, out _callerName);
                var guid = _clientModel.Connect(_callerName, _callerIP);
                var instructions = _commandModel.GetCommands();
                return Request.CreateResponse<List<Command>>(HttpStatusCode.OK, instructions);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        [HttpPost]
        [ActionName("Upload")]
        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new Exception("This operation needs a request content to be sent as MimeMultipartContent");

            var provider = new MultipartMemoryStreamProvider();

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                var content = provider.Contents.First();
                string filename = content.Headers.ContentDisposition.FileName;
                var buffer = await content.ReadAsByteArrayAsync();
                var stream = new MemoryStream(buffer);

                string filePath = Path.Combine(Path.GetTempPath(), filename);
                _fileModel.CreateFileFromStream(filePath, stream);

                _clientModel.GetUserData(out _callerIP, out _callerName);
                var client = _clientModel.GetClient(_callerName, _callerIP);
                _fileModel.AttachToClient(client.Id, filePath);

                return Request.CreateResponse(HttpStatusCode.OK, 
                    new ExecutionResult() { Value = true, ErrorsOrMessages = "Uploaded Sucessfully" });
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpDelete]
        [ActionName("Disconnect")]
        public HttpResponseMessage Delete()
        {
            try
            {
                var CallerIp = HttpContext.Current.Request.UserHostAddress;
                var CallerName = HttpContext.Current.Request.UserHostName;
                var result = _clientModel.Disconnect(CallerName, CallerIp);
                return Request.CreateResponse(HttpStatusCode.OK, 
                    new ExecutionResult() { Value = true, ErrorsOrMessages = "Disconnected Sucessfully" });
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        #endregion

    }
}
