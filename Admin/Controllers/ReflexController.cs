using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ReflexController : Controller
    {
        // GET: Reflex
        public ActionResult Index()
        {
            test();

            test2();

            return View();
        }

        #region 反射 => 获取对象的属性名称
        public void test()
        {
            var a = new Msg() { ID = 1, Name = "王科文" };
            var type = a.GetType().GetProperties();

            Dictionary<string, string> list = new Dictionary<string, string>();
            foreach (var item in type)
            {
                list.Add(item.Name, item.GetValue(a).ToString());
            }
        }

        public class Msg
        {
            public int ID { get; set; }

            public string Name { get; set; }
        }
        #endregion

        #region 反射 => 动态实例化接口
        public void test2()
        {
            IData info = Activator.CreateInstance(Type.GetType("Admin.Write", false, true)) as IData;
            info.write();
        }
        #endregion
    }
}