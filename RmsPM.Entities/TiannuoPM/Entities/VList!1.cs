namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    [Serializable]
    public class VList<T> : ListBase<T>
    {
        public VList()
        {
        }

        public VList(IList existingList)
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

        public override object Clone()
        {
            return this.Copy();
        }

        public virtual VList<T> Copy()
        {
            VList<T> list = new VList<T>();
            foreach (T local in Items)
            {
                T item = (T) ListBase<T>.MakeCopyOf(local);
                list.Add(item);
            }
            return list;
        }

        public virtual VList<T> FindAll(Enum column, object value)
        {
            return this.FindAll(column.ToString(), value, true);
        }

        public virtual VList<T> FindAll(string propertyName, object value)
        {
            return this.FindAll(propertyName, value, true);
        }

        public virtual VList<T> FindAll(Enum column, object value, bool ignoreCase)
        {
            return this.FindAll(column.ToString(), value, ignoreCase);
        }

        public virtual VList<T> FindAll(string propertyName, object value, bool ignoreCase)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T)).Find(propertyName, true);
            VList<T> list = new VList<T>();
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

        public virtual VList<T> FindAllBy(FindAllByType findAllByType, Enum column, object value)
        {
            return this.FindAllBy(findAllByType, column.ToString(), value, true);
        }

        public virtual VList<T> FindAllBy(FindAllByType findAllByType, string propertyName, object value)
        {
            return this.FindAllBy(findAllByType, propertyName, value, true);
        }

        public virtual VList<T> FindAllBy(FindAllByType findAllByType, Enum column, object value, bool ignoreCase)
        {
            return this.FindAllBy(findAllByType, column.ToString(), value, ignoreCase);
        }

        public virtual VList<T> FindAllBy(FindAllByType findAllByType, string propertyName, object value, bool ignoreCase)
        {
            PropertyDescriptor prop = base.PropertyCollection.Find(propertyName, true);
            VList<T> list = new VList<T>();
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

        public static void ForEach<U>(VList<U> list, Action<U> action)
        {
            list.ForEach(action);
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
        }

        [Serializable]
        public enum FindAllByType
        {
            Contains,EndsWith,StartsWith
        }
    }
}

