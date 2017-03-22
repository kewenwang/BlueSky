using Admin.App_Common;
using Common;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class TaskSchedulingController : Controller
    {
        // GET: TaskScheduling
        public ActionResult Index()
        {
            test();

            return View();
        }

        public void test()
        {
            //首先创建一个作业调度池
            ISchedulerFactory schedf = new StdSchedulerFactory();
            IScheduler sched = schedf.GetScheduler();
            //创建出来一个具体的作业

            IJobDetail job = JobBuilder.Create<JobDemo>().Build();
            //NextGivenSecondDate：如果第一个参数为null则表名当前时间往后推迟2秒的时间点。
            DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(DateTime.Now, 2);
            DateTimeOffset endTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddMinutes(2),2);
            //创建并配置一个触发器
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().StartAt(startTime).EndAt(endTime)
                                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).WithRepeatCount(3))
                                        .Build();
            //加入作业调度池中
            sched.ScheduleJob(job, trigger);

            sched.ResumeAll();

            //开始运行
            sched.Start();
        }


        public class JobDemo : IJob
        {
            /// <summary>
            /// 这里是作业调度每次定时执行方法
            /// </summary>
            /// <param name="context"></param>
            public void Execute(IJobExecutionContext context)
            {
                string url = "https://www.baidu.com/his?wd=&from=pc_web&rf=3&hisdata=%5B%7B%22time%22%3A1487211877%2C%22kw%22%3A%22%E9%9A%90%E5%96%BB%E8%83%BD%E5%8A%9B%22%7D%2C%7B%22time%22%3A1487298347%2C%22kw%22%3A%22angularjs%22%7D%2C%7B%22time%22%3A1487558245%2C%22kw%22%3A%22throw%22%7D%2C%7B%22time%22%3A1487558255%2C%22kw%22%3A%22.net%20throw%22%7D%2C%7B%22time%22%3A1487561163%2C%22kw%22%3A%22js%20typeof%E4%BB%80%E4%B9%88%E6%84%8F%E6%80%9D%22%7D%2C%7B%22time%22%3A1487561377%2C%22kw%22%3A%22js%20typeof%22%7D%2C%7B%22time%22%3A1487815043%2C%22kw%22%3A%22sq%3B%20char%E5%92%8Cnvarchar(50)%E7%9A%84%E5%8C%BA%E5%88%AB%22%7D%2C%7B%22time%22%3A1487828918%2C%22kw%22%3A%22webstorm%22%7D%2C%7B%22time%22%3A1487835018%2C%22kw%22%3A%22.net%20ado%20%E6%8F%92%E5%85%A5%E6%95%B0%E6%8D%AE%E5%A6%82%E4%BD%95%E5%88%A4%E6%96%AD%E6%B7%BB%E5%8A%A0%E6%88%90%E5%8A%9F%22%7D%5D&json=1&p=3&sid=1426_21089_22036_20928&req=2&csor=0&cb=jQuery1102041458626902098983_1488363069469&_=1488363069470";
                var data = RequestApi.DoGet(url);
                AppLog.Error(data.ResponseBody);
            }
        }
    }
}