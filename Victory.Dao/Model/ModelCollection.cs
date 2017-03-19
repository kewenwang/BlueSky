using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SqlConnect
{
    [Serializable]
    public class ModelCollection<T> :Collection<T>,IModelCollection,ICloneable
    {
        public event CollectionChangeDelegate<T> Change;

        public ModelCollection()
        {
            this.elementType = typeof(T);
        }

        public void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                this.Add(item);
            }
        }

        public void Sort(IComparer<T> comparer)
        {
            List<T> concreteList = new List<T>(this);
            concreteList.Sort(comparer);
            this.Clear();

            for (var i = 0; i < concreteList.Count; i++)
            {
                base.InsertItem(i, concreteList[i]);
            }
        }

        public void Sort(Comparison<T> comparison)
        {
            List<T> concreteList = new List<T>(this);
            concreteList.Sort(comparison);
            this.Clear();

            for (var i = 0; i < concreteList.Count; i++)
            {
                base.InsertItem(i, concreteList[i]);
            }
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            this.OnChange(item, CollectionChangeType.Added);
        }

        protected override void RemoveItem(int index)
        {
            T item = base[index];
            base.RemoveItem(index);
            this.OnChange(item, CollectionChangeType.Removed);
        }

        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            this.OnChange(item, CollectionChangeType.Updated);
        }

        private void OnChange(T item,CollectionChangeType type)
        {
            if (Change != null)
            {
                this.Change(item, type);
            }
        }

        #region IModelCollection 成员

        Type elementType;
        public Type ElementType
        {
            get
            {
                return elementType;
            }
        }

        #endregion

        #region ICloneable 成员

        public object Clone()
        {
            var newObject = new ModelCollection<T>();
            foreach(var item in Items)
            {
                newObject.Add(item);
            }
            return newObject;
        }

        #endregion       
    }

    public static class Ext
    {
        public static ModelCollection<T> ToModelCollection<T>(this IEnumerable<T> list) where T : IModel
        {
            ModelCollection<T> coll = new ModelCollection<T>();
            foreach (var item in list)
            {
                coll.Add(item);
            }
            return coll;
        }
    }
}
