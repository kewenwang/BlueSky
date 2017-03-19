using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlConnect
{
    public class SqlResource
    {
        internal SqlResource()
        {
            Sqls = new Dictionary<string, Sql>();
        }

        public string Url
        {
            get;
            set;
        }

        public void AddSql(Sql sql)
        {
            Sql otherSql;
            if (Sqls.TryGetValue(sql.Id, out otherSql))
            {
                string err = string.Format("资源 {0} 和资源 {1} 存在相同的sql id {2} ", otherSql.Own,sql.Own,sql.Id);
                throw new Exception(err);
            }

            this.Sqls.Add(sql.Id, sql);
            sql.Own = this;
        }

        internal Dictionary<string, Sql> Sqls
        {
            get;
            set;
        }
    }
}
