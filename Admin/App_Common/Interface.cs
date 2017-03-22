using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin
{
    public class App_Common
    {

    }

    public interface IData
    {
        void write();
    }
    public class Write : IData
    {
        public void write()
        {
            AppLog.Error("动态实例化调用接口");
        }
    }
}