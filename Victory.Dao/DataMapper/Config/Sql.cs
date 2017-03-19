using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlConnect
{
    public class Sql
    {
        public string Id
        {
            get;
            internal set;
        }

        public string Text
        {
            get;
            internal set;
        }

        internal SqlResource Own
        {
            get;
            set;
        }
    }
}
