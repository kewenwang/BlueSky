using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

using System.Data;

namespace SqlConnect
{
    internal class DataAccessSession :IDataAccessSession
    {
        internal DataAccessSession(DbConnection conn)
        {
            this.conn = conn;           
        }

        #region IDataAccessSession 成员


        private DbConnection conn;
        public DbConnection Connection
        {
            get 
            {
                return this.conn;
            }
        }

        private DbTransaction tran;
        public DbTransaction Transaction
        {
            get 
            {
                return this.tran;
            }
        }

        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        public void BeginTransaction(IsolationLevel level)
        {
            this.Connection.Open();
            this.tran = this.Connection.BeginTransaction(level);
        }

        public void CommitTranscation()
        {
            this.tran.Commit();
            this.Dispose();
        }

        public void RollbackTranscation()
        {
            this.tran.Rollback();
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this.tran.Dispose();
            this.conn.Close();
            this.conn.Dispose();
        }

        #endregion
    }
}
