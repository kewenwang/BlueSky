using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlConnect
{
    [Serializable]
    public abstract class BaseModel:IModel
    {
        public BaseModel()
        {
           
        }

        #region ICloneable 成员

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        #endregion
    }
}
