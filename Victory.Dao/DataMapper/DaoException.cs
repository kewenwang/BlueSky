using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlConnect
{
    public class DaoException:Exception
    {
        public DaoException()
        {
            
        }

        public DaoException(string message):base(message)
        {
            
        }

        public DaoException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
