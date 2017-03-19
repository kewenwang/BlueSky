using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Reflection;

using System.Collections;

namespace SqlConnect
{
    public class DataMapper:IDataMapper
    {
        public IDataHelper DataHelper
        {
            get;
            internal set;
        }

        public DaoConfig DaoConfig
        {
            get;
            internal set;
        }

        internal DataMapper(IDataHelper dataHelper, DaoConfig daoConfig)
        {
            this.DataHelper = dataHelper;
            this.DaoConfig = daoConfig;
            this.DateTimeKind = System.DateTimeKind.Unspecified;
        }

        public DateTimeKind DateTimeKind
        {
            get;
            set;
        }

        #region 接口方法

        public IDataAccessSession CreateSession()
        {
            IDataAccessSession session = this.DataHelper.CreateSession();
            return session;
        }

        public T Query<T>(string statementName, params object[] parameters) where T : new()
        {
            return Query<T>(statementName, null, null, parameters);
        }

        public T Query<T>(string statementName, IModel model) where T : new()
        {
            return Query<T>(statementName, null, null, model);
        }

        public T Query<T>(string statementName, Hashtable paramMap) where T : new()
        {
            return Query<T>(statementName, null, null, paramMap);
        }

        public T Query<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters) where T : new()
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, null, parameters);
            return Query<T>(unit);
        }

        public T Query<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model) where T : new()
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, model);
            return Query<T>(unit);
        }

        public T Query<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap) where T : new()
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, paramMap);
            return Query<T>(unit);
        }
        


        public ModelCollection<T> QueryCollection<T>(string statementName, params object[] parameters) where T : new()
        {
            return QueryCollection<T>(statementName, null, null, parameters);
        }

        public ModelCollection<T> QueryCollection<T>(string statementName, IModel model) where T : new()
        {
            return QueryCollection<T>(statementName, null, null, model);
        }

        public ModelCollection<T> QueryCollection<T>(string statementName, Hashtable paramMap) where T : new()
        {
            return QueryCollection<T>(statementName, null, null, paramMap);
        }

        public ModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters) where T : new()
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, parameters);
            return QueryCollection<T>(unit);
        }

        public ModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model) where T : new()
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, model);
            return QueryCollection<T>(unit);
        }

        public ModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap) where T : new()
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, paramMap);
            return QueryCollection<T>(unit);
        }

        public PagerModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, params object[] parameters) where T : new()
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, parameters);
            return QueryCollection<T>(unit, startIndex, endIndex);
        }

        public PagerModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, IModel model) where T : new()
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, model);
            return QueryCollection<T>(unit, startIndex, endIndex);
        }

        public PagerModelCollection<T> QueryCollection<T>(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, Hashtable paramMap) where T : new()
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, paramMap);
            return QueryCollection<T>(unit, startIndex, endIndex);
        }

       

        public DbDataReader ExecuteReader(string statementName, params object[] parameters)
        {
            return ExecuteReader(statementName, null, null, parameters);
        }

        public DbDataReader ExecuteReader(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, parameters);
            return ExecuteReader(unit);
        }

        public DbDataReader ExecuteReader(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, model);
            return ExecuteReader(unit);
        }

        public DbDataReader ExecuteReader(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, paramMap);
            return ExecuteReader(unit);
        }



        public object ExecuteScalar(string statementName, params object[] parameters)
        {
            return ExecuteScalar(statementName, null, null, parameters);
        }

        public object ExecuteScalar(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, parameters);
            return ExecuteScalar(unit);
        }

        public object ExecuteScalar(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, model);
            return ExecuteScalar(unit);
        }

        public object ExecuteScalar(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, paramMap);
            return ExecuteScalar(unit);
        }

        public int ExecuteNonQuery(string statementName, params object[] parameters)
        {
            return ExecuteNonQuery(statementName, null, null, parameters);
        }

        public int ExecuteNonQuery(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, parameters);
            return ExecuteNonQuery(unit);
        }

        public int ExecuteNonQuery(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, model);
            return ExecuteNonQuery(unit);
        }

        public int ExecuteNonQuery(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, paramMap);
            return ExecuteNonQuery(unit);
        }



        public DataTable ExecuteDatatable(string statementName, params object[] parameters)
        {
            return ExecuteDatatable(statementName, null, null, parameters);
        }

        public DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, parameters);
            return ExecuteDatatable(unit);
        }

        public DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, model);
            return ExecuteDatatable(unit);
        }

        public DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, paramMap);
            return ExecuteDatatable(unit);
        }

        public DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, params object[] parameters)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, parameters);
            return ExecuteDatatable(unit, startIndex, endIndex, out totalCount);
        }

        public DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, IModel model)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, model);
            return ExecuteDatatable(unit, startIndex, endIndex, out totalCount);
        }

        public DataTable ExecuteDatatable(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, Hashtable paramMap)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, paramMap);
            return ExecuteDatatable(unit, startIndex, endIndex, out totalCount);
        }



        public DataSet ExecuteDataset(string statementName, params object[] parameters)
        {
            return ExecuteDataset(statementName, null, null, parameters);
        }

        public DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, parameters);
            return ExecuteDataset(unit);
        }

        public DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, IModel model)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, model);
            return ExecuteDataset(unit);
        }

        public DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, paramMap);
            return ExecuteDataset(unit);
        }

        public DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, params object[] parameters)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, parameters);
            return ExecuteDataset(unit, startIndex, endIndex, out totalCount);
        }

        public DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, IModel model)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, model);
            return ExecuteDataset(unit, startIndex, endIndex, out totalCount);
        }

        public DataSet ExecuteDataset(string statementName, IDataAccessSession session, List<StatementCondition> conditions, int startIndex, int endIndex, out int totalCount, Hashtable paramMap)
        {
            Sql sql = DaoConfig.GetSql(statementName);
            ExecuteUnit unit = new ExecuteUnit(this, sql, session, conditions, paramMap);
            return ExecuteDataset(unit, startIndex, endIndex, out totalCount);
        }

        #endregion


        #region  内部方法

        private T Query<T>(ExecuteUnit unit) where T : new()
        {
            DataTable dt = this.DataHelper.ExecuteDataTable(unit.CommandText, unit.Session, unit.DbParams.ToArray());
            if (dt.Rows.Count == 0)
            {
                return default(T);
            }
            DataRow row = dt.Rows[0];
            T model = new T();

            this.FillModelValues(model, row);
            return model;
        }

        private ModelCollection<T> QueryCollection<T>(ExecuteUnit unit) where T : new()
        {
            DataTable dt = this.DataHelper.ExecuteDataTable(unit.CommandText, unit.Session, unit.DbParams.ToArray());
            ModelCollection<T> coll = new ModelCollection<T>();
            Type elementType = coll.ElementType;

            foreach (DataRow row in dt.Rows)
            {
                T model = DataRowToT<T>(row);
                coll.Add(model);
            }

            return coll;
        }

        private DataSet ExecuteDataset(ExecuteUnit unit)
        {
            DataSet ds = this.DataHelper.ExecuteDataSet(unit.CommandText, unit.Session, unit.DbParams.ToArray());
            return ds;
        }

        private PagerModelCollection<T> QueryCollection<T>(ExecuteUnit unit, int startIndex, int endIndex) where T : new()
        {
            int totalCount;

            DataTable dt = this.DataHelper.ExecuteDataTable(unit.CommandText,unit.Session, startIndex, endIndex, out totalCount, unit.DbParams.ToArray());
            PagerModelCollection<T> coll = new PagerModelCollection<T>(startIndex, endIndex, totalCount);
            Type elementType = coll.ElementType;

            foreach (DataRow row in dt.Rows)
            {
                T model = DataRowToT<T>(row);
                coll.Add(model);
            }

            return coll;
        }       
        
        private DbDataReader ExecuteReader(ExecuteUnit unit)
        {
            DbDataReader reader = this.DataHelper.ExecuteReader(unit.CommandText,unit.Session, unit.DbParams.ToArray());
            return reader;
        }

        private object ExecuteScalar(ExecuteUnit unit)
        {
            object obj = this.DataHelper.ExecuteScalar(unit.CommandText, unit.Session, unit.DbParams.ToArray());
            return obj;
        }

        private int ExecuteNonQuery(ExecuteUnit unit)
        {
            int count = this.DataHelper.ExecuteNonQuery(unit.CommandText, unit.Session, unit.DbParams.ToArray());
            return count;
        }

        private DataTable ExecuteDatatable(ExecuteUnit unit)
        {
            DataTable dt = this.DataHelper.ExecuteDataTable(unit.CommandText, unit.Session, unit.DbParams.ToArray());
            return dt;
        } 

        private DataTable ExecuteDatatable(ExecuteUnit unit, int startIndex, int endIndex, out int totalCount)
        {
            DataTable dt = this.DataHelper.ExecuteDataTable(unit.CommandText, unit.Session, startIndex, endIndex, out totalCount, unit.DbParams.ToArray());
            return dt;
        }

        private DataSet ExecuteDataset(ExecuteUnit unit, int startIndex, int endIndex, out int totalCount)
        {
            DataSet ds = this.DataHelper.ExecuteDataSet(unit.CommandText, unit.Session, startIndex, endIndex, out totalCount, unit.DbParams.ToArray());
            return ds;
        }


        private T DataRowToT<T>(DataRow row) where T : new()
        {
            var modelType = typeof(T);
            T model = (T)Activator.CreateInstance(modelType);

            if (modelType.IsValueType)
            {
                model = (T)row[0];
            }
            else
            {
                this.FillModelValues(model, row);
            }

            return model;
        }

        private void FillModelValues(object model, DataRow row)
        {
            Type modelType = model.GetType();
            PropertyInfo[] properties = modelType.GetProperties();
           
            foreach (var item in properties)
            {
                if (row.Table.Columns.Contains(item.Name))
                {
                    object value = row[item.Name];
                    SetPropertyValue(item, model, value);
                }
            }
        }

        private void SetPropertyValue(PropertyInfo propertyInfo, object model, object value)
        {
            if (value == DBNull.Value)
            {
                value = null;
            }

            if (propertyInfo.PropertyType == typeof(DateTime) && value != null)
            {
                value = DateTime.SpecifyKind((DateTime)value, this.DateTimeKind);
            }

            if (value != null && propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GenericTypeArguments[0].IsEnum)
            {
                var e = Enum.ToObject(propertyInfo.PropertyType.GenericTypeArguments[0], value);
                propertyInfo.SetValue(model, e, null);
            }
            else
            {
                propertyInfo.SetValue(model, value, null);
            }
        }

        #endregion
    
    }
}
