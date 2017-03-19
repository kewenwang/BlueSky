using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SqlConnect
{
    public class SqlServerAnalysis
    {
        public static SqlServerAnalysis Analyse(string sql)
        {
            SqlServerAnalysis sa = new SqlServerAnalysis();

            //sql = sql.ToLower();
            string pattern = @"order[\040\n\r]+by";
            MatchCollection coll = Regex.Matches(sql, pattern, RegexOptions.RightToLeft|RegexOptions.IgnoreCase);
            if (coll.Count > 0)
            {
                sa.select = sql.Substring(0, coll[0].Index - 1);
                sa.orderBy = sql.Substring(coll[0].Index);
            }
            else
            {
                sa.select = sql;
            }

            return sa;
        }

        private SqlServerAnalysis()
        {

        }

        private string select;
        public string Select
        {
            get
            {
                return select;
            }
        }

        private string orderBy;
        public string OrderBy
        {
            get
            {
                return orderBy;
            }
        }
    }
}
