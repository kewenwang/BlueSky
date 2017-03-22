using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Messaging;
using System.ServiceModel.Dispatcher;
using System.Threading;
using Common;

namespace Admin.Controllers
{
    public class MSMQController : Controller
    {
        // GET: MSMQ
        public ActionResult Index()
        {
            test();
            test2();
            return View();
        }

        #region 消息队列 => 基本创建 接受 处理
        public void test()
        {

            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(@".\Private$\MyQueues"))
            {
                messageQueue = new MessageQueue(@".\Private$\MyQueues");
                messageQueue.Label = "Testing Queue";
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\MyQueues");
                messageQueue.Label = "Newly Created Queue";
            }
            messageQueue.Send("First ever Message is sent to MSMQ", "Title");

            messageQueue.Formatter = new XmlMessageFormatter(new string[] { "System.String" });

            messageQueue.GetAllMessages();//返回队列中的所有消息

            foreach (Message msg in messageQueue)
            {
                string readMessage = msg.Body.ToString();

            }
            //messageQueue.Purge();
        }

        #endregion

        #region 消息队列 => 基本创建 接受 异步处理
        public void test2()
        {
            string msg = ".\\Private$\\WANGKEWEN";
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(msg))
            {
                messageQueue = new MessageQueue(msg);
                messageQueue.Label = "消息队列";
            }
            else
            {
                messageQueue = MessageQueue.Create(msg);
                messageQueue.Label = "消息队列";
            }

            using (var queue = messageQueue)
            {
                for (int i = 0; i < 1000; i++)
                {
                    var messagekey = string.Format("我是wangkewen{0}号", i);
                    var messageValue = string.Format("你的{0}号吗", i);
                    queue.Send(messageValue, messagekey);
                }

                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                var exist = false;

                while (!MessageQueue.Exists(msg))
                {
                    Console.WriteLine("No existing queue");
                }

                exist = true;

                List<string> list = new List<string>();

                new Thread(() => //线程处理
                {
                    while (exist)
                    {
                        var m = queue.Receive();//先进先出顺序，取出第一条数据并删除。
                        if (m != null && !string.IsNullOrEmpty(m.Body.ToString()))
                        {
                            AppLog.Error(m.Body.ToString());
                        }
                        else
                        {
                            exist = false; ;
                        }
                    }

                }).Start();
            }

        }
        #endregion
    }
}