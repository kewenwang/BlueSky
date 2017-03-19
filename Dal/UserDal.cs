using SqlConnect;
using SqlData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dal
{
    public class UserDal
    {
        public static int Add()
        {
            Hashtable hs = new Hashtable();
            hs.Add("Name","王科");
            int ID = Convert.ToInt32(SqlMapper.SqlServerInstance.ExecuteScalar("Add", null, null, hs));
            return ID;
        }

        public static int Delete()
        {
            int id = SqlMapper.SqlServerInstance.ExecuteNonQuery("Delete", 2);
            return id;
        }

        public static int Update()
        {
            int id = SqlMapper.SqlServerInstance.ExecuteNonQuery("Update","王小可",3);
            return id;
        }

        public static ModelCollection<User> GetList()
        {
            List<StatementCondition> conditions = new List<StatementCondition>();
            var hs = new Hashtable();
            ModelCollection<User> list = SqlMapper.SqlServerInstance.QueryCollection<User>("GetList",null,null,hs);
            return list;
        }

        public static PagerModelCollection<User> GetPageList()
        {
            var hs = new Hashtable();
            PagerModelCollection<User> list = SqlMapper.SqlServerInstance.QueryCollection<User>("GetList",null,null,1,3, hs);
            return list;
        }

        public static int AddCustom()
        {
            int id = SqlMapper.SqlServerInstance.DataHelper.ExecuteNonQuery("insert into Text values ('王科文');");
            return id;
        }
        public static int UpdateCustom()
        {
            int id = SqlMapper.SqlServerInstance.DataHelper.ExecuteNonQuery("Update Text set Name = '修改姓名' where ID = 5;");
            return id;
        }

    }

    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
