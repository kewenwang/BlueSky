using Model_EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dal;
using SqlConnect;
using Newtonsoft.Json;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            Ado();

            //Ef();

            return View();
        }

        #region Ado
        public void Ado()
        {
            int id = UserDal.UpdateCustom();
        }
        #endregion

        #region EF
        public void Ef()
        {
            NFineBaseEntities db = new NFineBaseEntities();
            var obj = db.Sys_Area.ToList();
        }
        #endregion

    }
}