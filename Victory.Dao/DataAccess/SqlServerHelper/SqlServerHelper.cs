using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace SqlConnect
{
    internal sealed class SqlServerHelper : BaseDataHelper, IDataHelper
    {
        public SqlServerHelper(string connectionStr)
            : base(connectionStr)
        {

        }


            
        #region IDataProviderFactory Members

        public override DbConnection CreateConnection()
        {
            var con= new SqlConnection();
            return con;
        }

        public override DbCommand CreateCommand()
        {
            var command= new SqlCommand();
            command.CommandTimeout = 0;
            return command;
        }

        public override DbParameter CreateParameter()
        {
            return new SqlParameter();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }

        #endregion


        public override DataSet ExecuteDataSet(string sql, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams)
        {
            return this.ExecuteDataSet(sql,null, startIndex, endIndex, out totalCount, dbParams);
        }

        public override DataSet ExecuteDataSet(string sql, IDataAccessSession session, int startIndex, int endIndex, out int totalCount, params DbParameter[] dbParams)
        {
            SqlServerAnalysis sa = SqlServerAnalysis.Analyse(sql);

            string countSql = "select count(*) from ( {0} ) as page#CountSql";
            countSql = string.Format(countSql, sa.Select);

            string rowNumberSql = "select  *,ROW_NUMBER() OVER ({0}) AS RowNumber from ({1}) as page#RowNumber";
            rowNumberSql = string.Format(rowNumberSql, sa.OrderBy, sa.Select);

            string filterRowNumberSql = "select * from ({0}) as filter@RowNumber WHERE RowNumber BETWEEN  {1}  AND  {2}  ";
            filterRowNumberSql = string.Format(filterRowNumberSql, rowNumberSql, startIndex, endIndex);

            string allSql = string.Format("{0};{1}", filterRowNumberSql, countSql);

            DataSet ds = this.ExecuteDataSet(allSql,session, dbParams);
            totalCount = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());
            return ds;
        }


        public override void BulkInsert(string tableName, DataTable dt)
        {
            using (SqlConnection con = this.BuildDbConnection(null) as SqlConnection)
            {
                con.Open();

                using (SqlBulkCopy sqlbc = new SqlBulkCopy(con, SqlBulkCopyOptions.UseInternalTransaction, null))
                {
                    sqlbc.DestinationTableName = tableName;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sqlbc.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                    }

                    try
                    {
                        sqlbc.WriteToServer(dt);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}