using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class InterfaceController : Controller
    {
        // GET: Interface
        public ActionResult Index()
        {

            Itest();

            return View();
        }

        #region 接口实例化_1
        public void Itest()
        {
            var a = new add();
            var text1 = a.Result(20, 10);

            var b = new Reduce();
            var text2 = b.Result(20, 10);

            var c = new Ride();
            var text3 = c.Result(20, 10);
        }


        public interface Basics
        {
            int Result(int x, int y);
        }

        public class add : Basics
        {
            public int Result(int x, int y)
            {
                return x + y;
            }
        }

        public class Reduce : Basics
        {
            public int Result(int x, int y)
            {
                return x - y;
            }
        }

        public class Ride : Basics
        {
            public int Result(int x, int y)
            {
                return x * y;
            }
        }
        #endregion
    }
}