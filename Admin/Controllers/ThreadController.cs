using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ThreadController : Controller
    {
        // GET: Thread
        public ActionResult Index()
        {
            test1();

            test2();

            test3();

            return View();
        }

        #region 多线程一
        public void test1()
        {
            ParameterizedThreadStart p = t =>
            {
                var o = (Tuple<int, EventWaitHandle>)t;
                AppLog.Error(string.Format("我是第{0}个线程", o.Item1.ToString()));
                o.Item2.Set();
            };

            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < 5; i++)
            {
                var handler = new ManualResetEvent(false);
                waits.Add(handler);

                Thread t = new Thread(p);
                t.Start(new Tuple<int, EventWaitHandle>(i, handler));
            }
        }
        #endregion

        #region 多线程二
        public void test2()
        {
            var waits = new List<EventWaitHandle>();
            List<string> list = new List<string>();
            int page = 5;
            for (int i = 0; i < page; i++)
            {
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(Print))
                {
                    Name = "thread" + i.ToString()

                }.Start(new Tuple<int, EventWaitHandle>(i, handler));
            }
            WaitHandle.WaitAll(waits.ToArray());
            AppLog.Error("终止");
        }

        private static void Print(object param)
        {
            var p = (Tuple<int, EventWaitHandle>)param;
            AppLog.Error(string.Format("我是第{0}个线程", p.Item1.ToString()));
            p.Item2.Set();
        }
        #endregion

        #region 多线程 回调函数

        public void test3()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                Thread t = new Thread(x => { list.Add(CallBackFun(i)); });
                t.Start();
                t.Join();
            }
            AppLog.Error("结束");
            AppLog.Error(list.Count.ToString());
        }

        public static string CallBackFun(int i)
        {
            AppLog.Error(string.Format("这里是回调函数内容，当前线程ID{0}",i.ToString()));
            return string.Format("这里是回调函数内容，当前线程ID{0}",i.ToString());
        }
        #endregion

    }
}