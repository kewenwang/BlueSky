using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace SqlConnect
{
    public interface IDataHelper:IDataProviderFactory
    {
        IDataAccessSession CreateSession();

                
        DataTable ExecuteDataTable(string sql, params DbParameter[] dbParams);
        DataTable ExecuteDataTable(string sql, IDataAccessSession session, params DbParameter[] dbParams);
        DataTable ExecuteDataTable(string sql, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams);
        DataTable ExecuteDataTable(string sql, IDataAccessSession session, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams);

                
        DataSet ExecuteDataSet(string sql, params DbParameter[] dbParams);
        DataSet ExecuteDataSet(string sql, IDataAccessSession session, params DbParameter[] dbParams);
        DataSet ExecuteDataSet(string sql, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams);
        DataSet ExecuteDataSet(string sql, IDataAccessSession session, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams);

                
        DbDataReader ExecuteReader(string sql, params DbParameter[] dbParams);
        DbDataReader ExecuteReader(string sql, IDataAccessSession session, params DbParameter[] dbParams);

                
        int ExecuteNonQuery(string sql, params DbParameter[] dbParams);
        int ExecuteNonQuery(string sql, IDataAccessSession session, params DbParameter[] dbParams);

                
        object ExecuteScalar(string sql, params DbParameter[] dbParams);
        object ExecuteScalar(string sql, IDataAccessSession session, params DbParameter[] dbParams);

    }
}
