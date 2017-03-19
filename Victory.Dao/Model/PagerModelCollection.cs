using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlConnect
{
    [Serializable]
    public class PagerModelCollection<T> : ModelCollection<T>, IPagerModelCollection
    {
        public PagerModelCollection(int startIndex, int endIndex, int totalRecord)
        {
            //todo:验证代码 
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            this.totalRecord = totalRecord;
        }

        private int startIndex;
        public int StartIndex
        {
            get
            {
                return startIndex;
            }          
        }

        private int endIndex;
        public int EndIndex
        {
            get
            {
                return endIndex;
            }            
        }

        private int totalRecord;
        public int TotalRecord
        {
            get
            {
                return totalRecord;
            }  
        }

    }
}
