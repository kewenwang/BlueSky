using System;
using System.Web.Mvc;
using Common;
using log4net;
using System.Reflection;
using log4net.Config;

namespace Admin.Controllers
{
    public class Log4netController : Controller
    {
        // GET: Log4net
        public ActionResult Index()
        {
            AppLog.Error("这是第一个日志");

            return View();
        }
    }
}