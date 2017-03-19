using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Text.RegularExpressions;


namespace SqlConnect
{   
    internal sealed class OleDbHelper : BaseDataHelper
    {
        public OleDbHelper(string connectionStr)
            : base(connectionStr)
        {

        }


        public override DataSet ExecuteDataSet(string sql, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams)
        {
            throw new NotImplementedException();
        }

        public override DataSet ExecuteDataSet(string sql, IDataAccessSession session, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams)
        {
            throw new NotImplementedException();
        }

        public override void BulkInsert(string tableName, DataTable dt)
        {
            throw new NotImplementedException();
        }

        public override DbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }

        public override DbCommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        public override DbParameter CreateParameter()
        {
            throw new NotImplementedException();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            throw new NotImplementedException();
        }
    }
}
