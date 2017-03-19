using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace SqlConnect
{
    public interface IDataAccessSession:IDisposable
    {
        DbConnection Connection
        {
            get;
        }

        DbTransaction Transaction
        {
            get;
        }

        void BeginTransaction();

        void BeginTransaction(IsolationLevel level);

        void CommitTranscation();

        void RollbackTranscation();
    }
}
