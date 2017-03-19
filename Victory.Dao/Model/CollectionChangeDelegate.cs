using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlConnect
{
    public delegate void CollectionChangeDelegate<T>(T item,CollectionChangeType type) ;
}
