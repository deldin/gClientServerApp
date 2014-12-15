using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace LogService.Controllers
{
    public class LogController : ApiController
    {
        // GET api/log
        public IEnumerable<LogItem> Get()
        {
            return new List<LogItem>();
        }

        // GET api/log/5
        public LogItem Get(int id)
        {
            return new LogItem();
        }

        
        // POST api/log
        public void Post([FromBody]LogItem value)
        {
        }

        // PUT api/log/5
        public void Put(int id, [FromBody]LogItem value)
        {
        }

        // DELETE api/log/5
        public void Delete(int id)
        {
        }
    }
}