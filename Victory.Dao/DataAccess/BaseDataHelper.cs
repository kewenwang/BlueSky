using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlTypes;

namespace SqlConnect
{
    internal abstract class BaseDataHelper:IDataHelper
    {
        private string connectionStr;

        public BaseDataHelper(string connectionStr)
        {
            this.connectionStr = connectionStr;
        }

        protected DbConnection BuildDbConnection(IDataAccessSession session)
        {
            DbConnection conn = null;
            if (session == null)
            {
                conn = this.CreateConnection();
                conn.ConnectionString = this.connectionStr;
            }
            else
            {
                conn = session.Connection;
            }
            return conn;
        }

        private DbCommand BuildDBCommand(IDataAccessSession session, CommandType commandType, string sql, params DbParameter[] dbParams)
        {
            DbConnection conn =this.BuildDbConnection(session);
            
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            
            DbCommand com = conn.CreateCommand();
            com.CommandType = commandType;
            com.CommandText = sql;

            if (session != null)
            {
                com.Transaction = session.Transaction;
            }

            if (dbParams != null)
            {
                foreach (DbParameter param in dbParams)
                {
                    com.Parameters.Add(param);
                }
            }

            return com;
        }

        private void ExecuteFinished(IDataAccessSession session, DbCommand com)
        {
            //if exist data transaction,can not close or dispose connection
            if (session == null)
            {
                com.Connection.Close();
                com.Connection.Dispose();
            }

            com.Parameters.Clear();
            com.Dispose();
        }

        #region IDataHelper 成员

        public IDataAccessSession CreateSession()
        {
            DbConnection conn = this.CreateConnection();
            conn.ConnectionString = this.connectionStr;

            return new DataAccessSession(conn);
        }

        #region ------------ExecuteDataTable------------

        public DataTable ExecuteDataTable(string sql, params DbParameter[] dbParams)
        {
            return this.ExecuteDataTable(sql, null, dbParams);
        }

        public DataTable ExecuteDataTable(string sql, IDataAccessSession session, params DbParameter[] dbParams)
        {
            DbCommand com = BuildDBCommand(session, CommandType.Text, sql, dbParams);
            DbDataReader reader = com.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Close();

            ExecuteFinished(session, com);

            return dt;
        }
                
        public DataTable ExecuteDataTable(string sql, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams)
        {
           return ExecuteDataTable(sql,null,startIndex,endIndex,out totalCount,dbParams);
        }

        public DataTable ExecuteDataTable(string sql, IDataAccessSession session, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams)
        {
            DataSet ds=this.ExecuteDataSet(sql,session, startIndex, endIndex, out totalCount, dbParams);
            
            return ds.Tables[0];
        }


        #endregion ------------ExecuteDataTable End------------


        #region ------------ExecuteDataSet------------

        public DataSet ExecuteDataSet(string sql, params DbParameter[] dbParams)
        {
            return this.ExecuteDataSet(sql,null, dbParams);
        }

        public DataSet ExecuteDataSet(string sql, IDataAccessSession session, params DbParameter[] dbParams)
        {
            DbCommand com = BuildDBCommand(session, CommandType.Text, sql, dbParams);
            DataSet ds = new DataSet();
            
            using (DbDataAdapter adapter=this.CreateDataAdapter())
            {
                adapter.SelectCommand = com;
                adapter.Fill(ds);
            }

            ExecuteFinished(session, com);
            return ds;   


        }

        public abstract DataSet ExecuteDataSet(string sql, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams);

        public abstract DataSet ExecuteDataSet(string sql, IDataAccessSession session, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams);

        #endregion ------------ExecuteDataSet End------------


        #region ------------ExecuteDataReader------------       

        public DbDataReader ExecuteReader(string sql, params DbParameter[] dbParams)
        {
            return this.ExecuteReader(sql,null, dbParams);
        }

        public DbDataReader ExecuteReader(string sql, IDataAccessSession session, params DbParameter[] dbParams)
        {
            DbCommand com = BuildDBCommand(session, CommandType.Text, sql, dbParams);
            DbDataReader reader = com.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }       

        #endregion ------------ExecuteDataReader End------------


        #region ------------ExecuteNonQuery------------
      
        public int ExecuteNonQuery(string sql, params DbParameter[] dbParams)
        {
            return this.ExecuteNonQuery(sql,null, dbParams);
        }

        public int ExecuteNonQuery(string sql, IDataAccessSession session, params DbParameter[] dbParams)
        {
            DbCommand com = BuildDBCommand(session, CommandType.Text, sql, dbParams);
            int result = com.ExecuteNonQuery();

            ExecuteFinished(session, com);
            
            return result;
        }

        #endregion ------------ExecuteNonQuery End------------


        #region ------------ExecuteScalar------------

        public object ExecuteScalar(string sql, params DbParameter[] dbParams)
        {
            return this.ExecuteScalar(sql,null, dbParams);
        }

        public object ExecuteScalar(string sql, IDataAccessSession session, params DbParameter[] dbParams)
        {
            DbCommand com = BuildDBCommand(session, CommandType.Text, sql, dbParams);
            object result = com.ExecuteScalar();

            ExecuteFinished(session, com);
            
            return result;
        }

        #endregion ------------ExecuteScalar End------------



        public abstract void BulkInsert(string tableName, DataTable dt);
     

        #endregion



        #region IDataProviderFactory 成员

        public abstract DbConnection CreateConnection();

        public abstract DbCommand CreateCommand();
       
        public abstract DbParameter CreateParameter();

        public abstract DbDataAdapter CreateDataAdapter();       

        #endregion
       
    }
}
