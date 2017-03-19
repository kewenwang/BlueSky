using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SqlConnect
{
    public interface IModelCollection:IModel,IList
    {
        Type ElementType
        {
            get;
        }
    }
}
