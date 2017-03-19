using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlConnect
{
    public class DaoConfig
    {
        internal DaoConfig()
        {            
            SqlDict = new Dictionary<string, Sql>();
        }

        public string ConnectionString
        {
            get;
            set;
        }

        private Dictionary<string, Sql> SqlDict;

        public void AddResource(SqlResource resource)
        {
            foreach (var item in resource.Sqls)
            {
                SqlDict.Add(item.Key, item.Value);
            }
        }

        public Sql GetSql(string id)
        {
            if (SqlDict.ContainsKey(id))
            {
                return SqlDict[id];
            }
            throw new DaoException(string.Format("Sql Id:{0} is not exist.", id));
        }
    }
}
