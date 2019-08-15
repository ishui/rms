namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Serializable]
    public class TList<T> : ListBase<T> where T: IEntity, new()
    {
        private List<T> deletedItems;

        public TList()
        {
        }

        public TList(IList existingList)
        {
            if (existingList != null)
            {
                foreach (T local in existingList)
                {
                    if (local != null)
                    {
                        base.Items.Add(local);
                    }
                }
            }
        }

        public virtual void AcceptChanges()
        {
            for (int i = 0; i < base.Count; i++)
            {
                base[i].AcceptChanges();
            }
        }

        public override object Clone()
        {
            return this.Copy();
        }

        public virtual TList<T> Copy()
        {
            TList<T> list = new TList<T>();
            foreach (T local in Items)
            {
                T item = (T) ListBase<T>.MakeCopyOf(local);
                list.Add(item);
            }
            return list;
        }

        public virtual void CopyTo(TList<T> copyTo)
        {
            ArrayList list = new ArrayList();
            foreach (T local in copyTo)
            {
                list.Add(local.ToString());
            }
            foreach (T local in Items)
            {
                T item = (T) ListBase<T>.MakeCopyOf(local);
                if (list.IndexOf(item.ToString()) < 0)
                {
                    copyTo.Add(item);
                }
            }
        }

        public bool Exists(Predicate<T> match)
        {
            return (this.FindIndex(match) != -1);
        }

        public TList<T> FindAll(Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }
            TList<T> list = new TList<T>();
            foreach (T local in base.Items)
            {
                if (match(local))
                {
                    list.Add(local);
                }
            }
            return list;
        }

        public virtual TList<T> FindAll(Enum column, object value)
        {
            return this.FindAll(column.ToString(), value, true);
        }

        public virtual TList<T> FindAll(string propertyName, object value)
        {
            return this.FindAll(propertyName, value, true);
        }

        public virtual TList<T> FindAll(Enum column, object value, bool ignoreCase)
        {
            return this.FindAll(column.ToString(), value, ignoreCase);
        }

        public virtual TList<T> FindAll(string propertyName, object value, bool ignoreCase)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T)).Find(propertyName, true);
            TList<T> list = new TList<T>();
            int start = 0;
            while (start > -1)
            {
                start = this.FindCore(prop, value, start, ignoreCase);
                if (start > -1)
                {
                    list.Add(base[start]);
                    start++;
                }
            }
            return list;
        }

        public virtual TList<T> FindAllBy(FindAllByType findAllByType, Enum column, object value)
        {
            return this.FindAllBy(findAllByType, column.ToString(), value, true);
        }

        public virtual TList<T> FindAllBy(FindAllByType findAllByType, string propertyName, object value)
        {
            return this.FindAllBy(findAllByType, propertyName, value, true);
        }

        public virtual TList<T> FindAllBy(FindAllByType findAllByType, Enum column, object value, bool ignoreCase)
        {
            return this.FindAllBy(findAllByType, column.ToString(), value, ignoreCase);
        }

        public virtual TList<T> FindAllBy(FindAllByType findAllByType, string propertyName, object value, bool ignoreCase)
        {
            PropertyDescriptor prop = base.PropertyCollection.Find(propertyName, true);
            TList<T> list = new TList<T>();
            int start = 0;
            while (start > -1)
            {
                start = this.FindAllBy(findAllByType, prop, value, start, ignoreCase);
                if (start > -1)
                {
                    list.Add(base[start]);
                    start++;
                }
            }
            return list;
        }

        protected virtual int FindAllBy(FindAllByType findAllByType, PropertyDescriptor prop, object key, int start, bool ignoreCase)
        {
            for (int i = start; i < base.Count; i++)
            {
                T component = base[i];
                object obj2 = prop.GetValue(component);
                if ((key == null) && (obj2 == null))
                {
                    return i;
                }
                if (obj2 is string)
                {
                    switch (findAllByType)
                    {
                        case FindAllByType.StartsWith:
                            if (!obj2.ToString().StartsWith(key.ToString(), ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture))
                            {
                                break;
                            }
                            return i;

                        case FindAllByType.EndsWith:
                            if (!obj2.ToString().EndsWith(key.ToString(), ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture))
                            {
                                break;
                            }
                            return i;

                        case FindAllByType.Contains:
                            if (!obj2.ToString().Contains(key.ToString()))
                            {
                                break;
                            }
                            return i;
                    }
                }
                else if ((obj2 != null) && obj2.Equals(key))
                {
                    return i;
                }
            }
            return -1;
        }

        public int FindIndex(Predicate<T> match)
        {
            return this.FindIndex(0, base.Count, match);
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return this.FindIndex(startIndex, base.Count - startIndex, match);
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            if (startIndex > base.Count)
            {
                throw new ArgumentOutOfRangeException("startIndex", "index is out of range");
            }
            if ((count < 0) || (startIndex > (base.Count - count)))
            {
                throw new ArgumentOutOfRangeException("count", "count is out of range");
            }
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }
            int num = startIndex + count;
            for (int i = startIndex; i < num; i++)
            {
                if (match(base[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public static void ForEach<U>(TList<U> list, Action<U> action) where U: IEntity, new()
        {
            list.ForEach(action);
        }

        public TList<T> GetRange(int index, int count)
        {
            if ((index < 0) || (count < 0))
            {
                throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "ArgumentOutOfRange_NeedNonNegNum");
            }
            TList<T> list = new TList<T>();
            for (int i = index; (i < (index + count)) && (i < base.Count); i++)
            {
                list.Add(base.Items[i]);
            }
            return list;
        }

        protected override void InsertItem(int index, T item)
        {
            item.ParentCollection = this;
            base.InsertItem(index, item);
        }

        public void RemoveEntity(T item)
        {
            this.RemoveEntity(base.Items.IndexOf(item));
        }

        public void RemoveEntity(int index)
        {
            this.RemoveItem(index);
        }

        protected override void RemoveItem(int index)
        {
            T item = base.Items[index];
            if (item != null)
            {
                if (item.EntityState != EntityState.Added)
                {
                    item.MarkToDelete();
                    this.DeletedItems.Add(item);
                }
                base.RemoveItem(index);
            }
        }

        public List<T> DeletedItems
        {
            get
            {
                if (this.deletedItems == null)
                {
                    this.deletedItems = new List<T>();
                }
                return this.deletedItems;
            }
        }

        public List<T> InvalidItems
        {
            get
            {
                List<T> list = new List<T>();
                foreach (T local in Items)
                {
                    if (!local.IsValid)
                    {
                        list.Add(local);
                    }
                }
                return list;
            }
        }

        public virtual int IsDeletedCount
        {
            get
            {
                return this.DeletedItems.Count;
            }
        }

        public virtual bool IsDirty
        {
            get
            {
                return (((this.IsNewCount > 0) || (this.IsDeletedCount > 0)) || (this.IsDirtyCount > 0));
            }
        }

        public virtual int IsDirtyCount
        {
            get
            {
                int num = 0;
                foreach (T local in Items)
                {
                    if (local.EntityState == EntityState.Changed)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public virtual int IsNewCount
        {
            get
            {
                int num = 0;
                foreach (T local in Items)
                {
                    if (local.EntityState == EntityState.Added)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public virtual bool IsValid
        {
            get
            {
                foreach (T local in Items)
                {
                    if (!local.IsValid)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        [Serializable]
         public enum FindAllByType
        {
            Contains,EndsWith,StartsWith
        }
    }
}

