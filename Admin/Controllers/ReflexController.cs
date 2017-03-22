using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Common;

namespace Admin.Controllers
{
    public class ReflexController : Controller
    {
        // GET: Reflex
        public ActionResult Index()
        {
            //test();

            //test2();

            //test3();

            //test4();

            test5();

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

        #region 反射 => 获取类的参数值和方法名

        public void test3()
        {

            Type t = typeof(MyClass);   //获取描述MyClass类型的Type对象
            AppLog.Error("Analyzing methods in " + t.Name);  //t.Name="MyClass"

            //两种不同的获取方式
            //MethodInfo[] mi = t.GetMethods();  //MethodInfo对象在System.Reflection命名空间下。
            MethodInfo[] mi = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);  //不获取继承方法，为实例方法，为公开的

            foreach (MethodInfo m in mi) //遍历mi对象数组
            {
                AppLog.Error(m.ReturnType.Name); //返回方法的返回类型
                AppLog.Error(m.Name); //返回方法的名称

                ParameterInfo[] pi = m.GetParameters();  //获取方法参数列表并保存在ParameterInfo对象数组中
                for (int i = 0; i < pi.Length; i++)
                {
                    AppLog.Error(pi[i].ParameterType.Name); //方法的参数类型名称
                    AppLog.Error(pi[i].Name);  // 方法的参数名
                }
            }
        }

        public class MyClass
        {
            int x; int y;
            public int sum()
            {
                return x + y;
            }
            public void Show()
            {
                Console.WriteLine("x:{0},y:{1}", x, y);
            }
        }
        #endregion

        #region 反射 => 动态调用类里面的方法

        public void test4()
        {
            Type t = typeof(MyClass);
            MyClass1 reflectOb = new MyClass1();

            MethodInfo[] mi = t.GetMethods();
            foreach (MethodInfo m in mi)
            {
                ParameterInfo[] pi = m.GetParameters();

                if (m.Name.Equals("Set", StringComparison.Ordinal) && pi[0].ParameterType == typeof(string) && pi[1].ParameterType == typeof(string))
                {
                    object[] obj = new object[2];
                    obj[0] = "90";
                    obj[1] = "10";
                    //参数reflectOb,为一个对象引用，将调用他所指向的对象上的方法，如果为静态方法这个参数必须设置为null
                    //参数args，为调用方法的参数数组，如果不需要参数为null
                    m.Invoke(reflectOb, obj);   //调用MyClass类中的参数类型为int的Set方法，输出为Inside set(int,int).x:9, y:10
                }
            }
        }

        public class MyClass1
        {
            public void Set(string a, string b)
            {
                AppLog.Error(a + b);
            }
        }
        #endregion

        #region 反射 => 动态加载程序集（demo为 DLL 文件）
        public void test5()
        {

            #region 动态加载程序集,调用方法

            Assembly asm = Assembly.LoadFrom(@"D:\sky\BlueSky\packages\Newtonsoft.Json.6.0.4\lib\net45\Newtonsoft.Json.dll");  //加载指定的程序集

            Type tt = asm.GetType("Newtonsoft.Json.JsonConvert");

            MemberInfo[] mm = tt.GetMembers();

            foreach (MethodInfo item in mm)
            {
                if (item.Name.Equals("SerializeObject", StringComparison.Ordinal))
                {
                    object[] par = new object[1];
                    par[0] = "天子城";
                    var data = item.Invoke(null, par);
                    AppLog.Error(data + "xxx");
                    break;
                }
            }
            #endregion

            Type t = typeof(MyClass3);
            int val;
            ConstructorInfo[] ci = t.GetConstructors();  //使用这个方法获取构造函数列表

            int x;
            for (x = 0; x < ci.Length; x++)
            {
                ParameterInfo[] pi = ci[x].GetParameters(); //获取当前构造函数的参数列表
                if (pi.Length == 2) break;    //如果当前构造函数有2个参数，则跳出循环
            }

            if (x == ci.Length)
            {
                return;
            }

            object[] consargs = new object[2];
            consargs[0] = 10;
            consargs[1] = 20;
            object reflectOb = ci[x].Invoke(consargs);  //实例化一个这个构造函数有两个参数的类型对象,如果参数为空，则为null

            //实例化后，调用MyClass3中的方法
            MethodInfo[] mi = t.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            foreach (MethodInfo m in mi)
            {
                if (m.Name.Equals("sum", StringComparison.Ordinal))
                {
                    val = (int)m.Invoke(reflectOb, null);  //由于实例化类型对象的时候是用的两个参数的构造函数，所以这里返回的结构为30
                    AppLog.Error(" sum is " + val);  //输出 sum is 30
                }
            }

        }

        public class MyClass3
        {
            int x;
            int y;
            public MyClass3(int i)
            {
                x = y + i;
            }
            public MyClass3(int i, int j)
            {
                x = i;
                y = j;
            }
            public int sum()
            {
                return x + y;
            }
        }
        #endregion

    }
}