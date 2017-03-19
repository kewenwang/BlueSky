using SqlConnect;
using System.Reflection;

namespace SqlData
{
    public static class SqlMapper
    {

        private static readonly IDataMapper sqlServer;

        static SqlMapper()
        {
            sqlServer = DataMapperProvider.Build("SqlConfig.xml", Assembly.GetExecutingAssembly(), "connectionString");
        }

        public static IDataMapper SqlServerInstance
        {
            get
            {
                return sqlServer;
            }
        }
    }
}
