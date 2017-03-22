using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class WebApiController : Controller
    {
        // GET: WebApi
        public ActionResult Index()
        {   

            //相关调用在页面!

            return View();
        }
    }
}