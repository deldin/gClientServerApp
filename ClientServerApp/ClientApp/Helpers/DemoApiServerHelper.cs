using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;


namespace ClientApp
{
    public class ApiServerHelper : IDisposable
    {
        private string _url = "";
        HttpClient _client;
        List<MediaTypeFormatter> _formatters;
        private bool disposed = false;

        public ApiServerHelper(string url)
        {
            this._url = url;
            this._client = new HttpClient();
            _client.BaseAddress = new Uri(this._url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            this._formatters = new List<MediaTypeFormatter>() { new XmlMediaTypeFormatter() };
        }

        public List<Command> Connect()
        {
            HttpResponseMessage response = _client.GetAsync("api/Compiler/Connect").Result;
            var data = response.Content.ReadAsAsync<List<Command>>(_formatters).Result;
            return data;
        }

        public ExecutionResult Upload(string inputValues)
        {
            var task = UploadAsync(inputValues);            
            return task.Result;
        }

        private async Task<ExecutionResult> UploadAsync(string inputValues)
        {
           using(var client = new  HttpClient())
           {
                using (var formData = new MultipartFormDataContent())
                {
                    var filepath = inputValues;
                    HttpContent content = new ByteArrayContent(System.IO.File.ReadAllBytes(filepath));
                    formData.Add(content, "File", Path.GetFileName(filepath));

                    client.BaseAddress = new Uri(this._url);
                    var response = await client.PostAsync("api/Compiler/Compile", formData);
                    var result = await response.Content.ReadAsAsync<ExecutionResult>();
                    response.EnsureSuccessStatusCode();
                    return result;
                }
           }
        }

        public ExecutionResult Compile()
        {
            var task = CompileAsync();            
            return task.Result;
        }

        private async Task<ExecutionResult> CompileAsync()
        {
            HttpResponseMessage response = await _client.PutAsync("api/Compiler/Compile", null);
            var result = await response.Content.ReadAsAsync<ExecutionResult>();
            response.EnsureSuccessStatusCode();
            return result;
        }

        public ExecutionResult RunTests()
        {
            var task = RunTestsAsync();            
            return task.Result;
        }

        private async Task<ExecutionResult> RunTestsAsync()
        {
            HttpResponseMessage response = _client.PutAsync("api/Test/RunTests", null).Result;
            var result = await response.Content.ReadAsAsync<ExecutionResult>();
            response.EnsureSuccessStatusCode();
            return result;
        }

        public ExecutionResult Disconnect()
        {
            var task = DisconnectAsync();
            return task.Result;
        }

        private async Task<ExecutionResult> DisconnectAsync()
        {
            HttpResponseMessage response = _client.DeleteAsync("api/Compiler/Disconnect").Result;
            var result = await response.Content.ReadAsAsync<ExecutionResult>();
            response.EnsureSuccessStatusCode();
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _client.Dispose();
                    _formatters = null;
                }

                // shared cleanup logic
                disposed = true;
            }
        }

        ~ApiServerHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
