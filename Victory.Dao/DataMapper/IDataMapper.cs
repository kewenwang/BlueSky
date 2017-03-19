using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace SqlConnect
{
    public interface IDataMapper
    {
        IDataAccessSession CreateSession();

        IDataHelper DataHelper
        {
            get;
        }

        DateTimeKind DateTimeKind
        {
            get;
            set;
        }
       
        #region --------------Query-------------------

        T Query<T>(string statementName, params object[] parameters) where T :  new();
        T Query<T>(string statementName, IModel model) where T :  new();
        T Query<T>(string statementName, Hashtable paramMap) where T :  new();

        T Query<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters) where T : new();
        T Query<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model) where T : new();
        T Query<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap) where T : new();

        ModelCollection<T> QueryCollection<T>(string statementName, params object[] parameters) where T :  new();
        ModelCollection<T> QueryCollection<T>(string statementName, IModel model) where T :  new();
        ModelCollection<T> QueryCollection<T>(string statementName, Hashtable paramMap) where T :  new();

        ModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters) where T : new();
        ModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model) where T : new();
        ModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap) where T : new();

        PagerModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, params object[] parameters) where T : new();
        PagerModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, IModel model) where T : new();
        PagerModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, Hashtable paramMap) where T : new();

        #endregion
       

        #region --------------ExecuteReader-------------------

        DbDataReader ExecuteReader(string statementName, params object[] parameters);
        DbDataReader ExecuteReader(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters);
        DbDataReader ExecuteReader(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model);
        DbDataReader ExecuteReader(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap);

        #endregion

        #region --------------ExecuteScalar-------------------

        object ExecuteScalar(string statementName, params object[] parameters);
        object ExecuteScalar(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters);
        object ExecuteScalar(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model);
        object ExecuteScalar(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap);

        #endregion

        #region --------------ExecuteNonQuery-------------------

        int ExecuteNonQuery(string statementName, params object[] parameters);
        int ExecuteNonQuery(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters);
        int ExecuteNonQuery(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model);
        int ExecuteNonQuery(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap);

        #endregion

        #region --------------ExecuteDatatable-------------------

        DataTable ExecuteDatatable(string statementName, params object[] parameters);
        DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters);
        DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model);
        DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap);
        DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, params object[] parameters);
        DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, IModel model);
        DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, Hashtable paramMap);

        #endregion

        #region --------------ExecuteDataset-------------------

        DataSet ExecuteDataset(string statementName, params object[] parameters);
        DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters);
        DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model);
        DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap);
        DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, params object[] parameters);
        DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, IModel model);
        DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, Hashtable paramMap);

        #endregion

    }
}
