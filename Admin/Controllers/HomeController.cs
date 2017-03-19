using Model_EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dal;
using SqlConnect;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {

        #region EF
        //NFineBaseEntities db = new NFineBaseEntities(); => EF
        //var obj = db.Sys_Area.ToList();  => EF
        #endregion

        public ActionResult Index()
        {   
            int id = UserDal.UpdateCustom();

            string aa = "";

            return View();
        }
    }
}