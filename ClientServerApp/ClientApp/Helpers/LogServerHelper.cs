using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ClientApp
{
    public class LogServerHelper
    {
        private string url = "";
        HttpClient client = new HttpClient();

        public LogServerHelper(string url)
        {
            this.url = url;
        }

        public List<LogItem> Get()
        {
            HttpResponseMessage response = client.GetAsync(this.url).Result;
            var data = response.Content.ReadAsAsync<List<LogItem>>().Result;
            return data;
        }

        public string Post(LogItem obj)
        {
            return "";
        }

        public string Put(string Id, LogItem obj)
        {
            return "";
        }

        public string Delete(string Id)
        {
            return "";
        }
    }
}
