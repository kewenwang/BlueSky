using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

using System.Reflection;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SqlConnect
{
    public class ExecuteUnit
    {
        public Sql Sql
        {
            get;
            private set;
        }

        public DataMapper DataMapper
        {
            get;
            set;
        }

        public IDataAccessSession Session
        {
            get;
            set;
        }

        public string CommandText
        {
            get;
            private set;
        }

        public List<DbParameter> DbParams
        {
            get;
            private set;
        }

        public ExecuteUnit(DataMapper dataMapper, Sql sql, IDataAccessSession session, List<StatementCondition> conditions)
        {
            DbParams = new List<DbParameter>();

            this.DataMapper = dataMapper;            
            this.Sql = sql;
            this.Session = session;
            this.Filter(conditions);
        }

        public ExecuteUnit(DataMapper dataMapper, Sql sql, IDataAccessSession session, List<StatementCondition> conditions, params object[] parameters)
            : this(dataMapper, sql,session, conditions)
        {           
            this.ProcessParamters(conditions, parameters);
        }

        public ExecuteUnit(DataMapper dataMapper, Sql sql, IDataAccessSession session, List<StatementCondition> conditions, IModel model)
            : this(dataMapper, sql,session, conditions)
        {            
            this.ProcessParamters(conditions, model);
        }

        public ExecuteUnit(DataMapper dataMapper, Sql sql, IDataAccessSession session, List<StatementCondition> conditions, Hashtable paramMap)
            : this(dataMapper, sql,session, conditions)
        {
            this.ProcessParamters(conditions, paramMap);
        }

        private void ProcessParamter(StringParamater sp, object paramValue)
        {
            if (sp.Prefix == "#")
            {
                DbParameter dbparam = CreateDbParamter(sp.Name, paramValue);
                this.DbParams.Add(dbparam);
                this.CommandText = this.CommandText.Replace(sp.ToString(), "@" + sp.Name);
            }
            else if (sp.Prefix == "$")
            {
                if (paramValue == null)
                {
                    throw new ArgumentException(string.Format("参数${0}$，其值不能为null.",sp.Name));
                }

                this.CommandText = this.CommandText.Replace(sp.ToString(), paramValue.ToString());
            }
        }

        private void ProcessParamters(List<StatementCondition> conditons, params object[] parameters)
        {
            var pcoll = GetStringParams(conditons);
            if (pcoll.Count != parameters.Length)
            {
                string error = string.Format("Sql语句{0}中要求的参数有{1}个,而实际传入的参数有{2}个.", Sql.Id,pcoll.Count,parameters.Length);
                throw new DaoException(error);
            }

            for (int i = 0; i < pcoll.Count; i++)
            {
                ProcessParamter(pcoll[i], parameters[i]);
            }
        }

        private void ProcessParamters(List<StatementCondition> conditons, IModel model)
        {
            var pcoll = GetStringParams(conditons);
            Type modelType = model.GetType();           

            for (int i = 0; i < pcoll.Count; i++)
            {
                PropertyInfo pi = modelType.GetProperty(pcoll[i].Name);
                object paramValue = pi.GetValue(model, null);
                ProcessParamter(pcoll[i], paramValue);
            }
        }

        private void ProcessParamters(List<StatementCondition> conditons, Hashtable htable)
        {
            var pcoll = GetStringParams(conditons);
            for (int i = 0; i < pcoll.Count; i++)
            {
                object paramValue = htable[pcoll[i].Name];
                ProcessParamter(pcoll[i], paramValue);
            }
        }

        private DbParameter CreateDbParamter(string paramName, object paramValue)
        {
            DbParameter param = this.DataMapper.DataHelper.CreateParameter();
            param.ParameterName = paramName;
            if (paramValue == null)
            {
                paramValue = DBNull.Value;
            }

            param.Value = paramValue;
            return param;
        }

        private StringParamaterCollection GetStringParams(List<StatementCondition> conditons)
        {
            string paramReg = @"(?<Prefix>[#|$])(?<ParamName>[\w]+)\k<Prefix>";
            MatchCollection coll = Regex.Matches(this.CommandText, paramReg);

            StringParamaterCollection pcoll = new StringParamaterCollection();

            for (int i = 0; i < coll.Count; i++)
            {
                string prefix = coll[i].Groups["Prefix"].Value;
                string paramName = coll[i].Groups["ParamName"].Value;
                pcoll.Add(new StringParamater(prefix, paramName));
            }
            return pcoll;
        }

        private void Filter(List<StatementCondition> conditions)
        {
            //XDeclaration xde = new XDeclaration("1.0", "utf-8", "yes");
            XDocument doc = XDocument.Parse(this.Sql.Text);
            //XElement e = new XElement("sql",this.Sql.Text);

            Filter(conditions, doc);
            this.CommandText = doc.Root.Value.Replace("{", "<").Replace("}", ">");
        }

        private void Filter(List<StatementCondition> conditions, XDocument doc)
        {
            List<XElement> xConditions = doc.Root.Descendants("condition").ToList();

            for (int i = 0; i < xConditions.Count; i++)
            {
                XElement item = xConditions[i];

                XAttribute att = null;
                string err = string.Empty;

                att = item.Attribute("property");
                if (att == null)
                {
                    err = string.Format("在SqlResource{0}的语句{1}中，Condition缺少property属性", this.Sql.Own.Url,this.Sql.Id);
                    throw new DaoException(err);
                }

                string propertyName = att.Value;
                StatementCondition condition = conditions.FirstOrDefault(T => T.Name == propertyName && T.Enabled == true);
                if (condition == null)
                {
                    item.Remove();
                }
            }
        }
    }

    internal class StringParamater
    {

        public StringParamater(string prefix,string name)
        {
            this.Prefix=prefix;
            this.Name=name;
        }

        public string Prefix
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{0}",this.Prefix,this.Name);
        }
    }

    internal class StringParamaterCollection:Collection<StringParamater>
    {   
        protected override void InsertItem(int index, StringParamater item)
        {
            var t=this.FirstOrDefault(T => T.Prefix == item.Prefix && T.Name == item.Name);
            if (t == null)
            {
                base.InsertItem(index, item);
            }
        }
    }
}
