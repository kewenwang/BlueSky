using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SqlConnect
{
    public static class DataMapperProvider
    {
        public static DataMapper Build(string configFile,Assembly sqlSourceAssembly,string connKey)
        {
            DaoConfig daoConfig = DaoConfigProvider.Build(sqlSourceAssembly, configFile, connKey);
            IDataHelper helper = DataHelperFactory.Create(SqlConnectType.SqlServer, daoConfig.ConnectionString);

            return new DataMapper(helper, daoConfig);
        }
    }
}
