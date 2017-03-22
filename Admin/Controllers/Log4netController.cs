using System;
using System.Web.Mvc;
using Common;

namespace Admin.Controllers
{
    public class Log4netController : Controller
    {
        // GET: Log4net
        public ActionResult Index()
        {
            test();

            return View();
        }

        public void test()
        {
            AppLog.Error("这是第一个日志");
        }
    }
}