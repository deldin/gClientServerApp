using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<LogItem> list = new List<LogItem>();
            list.Add(new LogItem() { Id = Guid.NewGuid(), LogType = "Error", LogText = "Test", Source = "AnySource", Created = DateTime.Now });
            list.Add(new LogItem() { Id = Guid.NewGuid(), LogType = "Error", LogText = "Test1", Source = "AnySource", Created = DateTime.Now });
            list.Add(new LogItem() { Id = Guid.NewGuid(), LogType = "Error", LogText = "Test2", Source = "AnySource", Created = DateTime.Now });
            list.Add(new LogItem() { Id = Guid.NewGuid(), LogType = "Error", LogText = "Test3", Source = "AnySource", Created = DateTime.Now });
            return View(list);
        }
    }
}
