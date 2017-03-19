using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlConnect
{
    public class DataHelperFactory
    {
        public static IDataHelper Create(SqlConnectType dbType,string connStr)
        {
            switch (dbType)
            {
                case SqlConnectType.SqlServer: return new SqlServerHelper(connStr);
                case SqlConnectType.Orcale: return new SqlServerHelper(connStr);
                case SqlConnectType.Odbc: return new OdbcHelper(connStr);
                case SqlConnectType.Oledb: return new OleDbHelper(connStr);

                default: throw new System.Configuration.ConfigurationErrorsException("暂不支持此类型的数据库.");
            }
        }
    }

    public enum SqlConnectType
    {
        SqlServer=1,
        Orcale=2,
        Odbc=3,
        Oledb=4

    }
}
