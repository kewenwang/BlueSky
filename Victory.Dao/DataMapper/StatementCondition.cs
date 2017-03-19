using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlConnect
{
    public class StatementCondition
    {
        public StatementCondition()
        {         

        }

        public StatementCondition(string name, bool enabled)
        {
            this.Name = name;
            this.Enabled = enabled;
        }

        public string Name
        {
            get;
            set;
        }
        public bool Enabled
        {
            get;
            set;
        }
    }
}
