namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class ListBase<T> : BindingList<T>, IBindingListView, IBindingList, IList, ICollection, IEnumerable, ICloneable, IListSource, ITypedList, IDisposable, IRaiseItemChangedEvents, IDeserializationCallback
    {
        [NonSerialized]
        private Dictionary<string, PropertyDescriptorCollection> _childCollectionProperties;
        private bool _containsListCollection;
        private string _filterString;
        private bool _isSorted;
        private string _listName;
        private List<T> _OriginalList;
        [NonSerialized]
        private PropertyDescriptorCollection _propertyCollection;
        private ISite _site;
        [NonSerialized]
        private ListSortDescriptionCollection _sortDescriptions;
        private ListSortDirection _sortDirection;
        [NonSerialized]
        private PropertyDescriptor _sortProperty;
        private EventHandler Disposed;
        private List<T> excludedItems;


        public ListBase()
        {
            this._OriginalList = new List<T>();
            this._isSorted = false;
            this._sortDirection = ListSortDirection.Descending;
            this._sortDescriptions = new ListSortDescriptionCollection();
            this._filterString = null;
            this.excludedItems = new List<T>();
            this._containsListCollection = false;
            this._site = null;
            this.InitializeList();
        }

        public void AddRange(T[] array)
        {
            if (array != null)
            {
                foreach (T local in array)
                {
                    base.Add(local);
                }
            }
        }

        public void AddRange(List<T> list)
        {
            if (list != null)
            {
                foreach (T local in list)
                {
                    base.Add(local);
                }
            }
        }

        public void AddRange(ListBase<T> list)
        {
            if (list != null)
            {
                foreach (T local in list)
                {
                    base.Add(local);
                }
            }
        }

        internal DataSet AddRelations(DataSet dataset, List<DataColumn> parentKeys, bool includeChildren)
        {
            if (dataset == null)
            {
                throw new ArgumentException("Invalid parameter context, dataset can not be null in this method context.", "dataset");
            }
            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            DataTable table = null;
            bool flag = false;
            string name = typeof(T).Name;
            if (!dataset.Tables.Contains(name))
            {
                table = new DataTable(name);
                flag = false;
            }
            else
            {
                table = dataset.Tables[name];
                flag = true;
            }
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            List<DataColumn> list2 = new List<DataColumn>();
            if (!flag)
            {
                foreach (PropertyDescriptor descriptor in properties)
                {
                    if (descriptor.Attributes.Matches(new Attribute[] { new ReadOnlyAttribute(false), new TiannuoPM.Entities.BindableAttribute(), new DescriptionAttribute() }))
                    {
                        list2.Add(table.Columns.Add(descriptor.Name, descriptor.PropertyType));
                    }
                    else if (((descriptor.PropertyType.GetInterface("IList") != null) && descriptor.PropertyType.IsGenericType) && ((descriptor.PropertyType.BaseType != null) && (descriptor.PropertyType.BaseType.GetGenericTypeDefinition() == typeof(ListBase<>))))
                    {
                        list.Add(descriptor);
                    }
                    else if ((descriptor.PropertyType.GetInterface("IEntity") == null) && descriptor.Attributes.Matches(new Attribute[] { new TiannuoPM.Entities.BindableAttribute(), new DescriptionAttribute() }))
                    {
                        Type type = (descriptor.PropertyType.IsGenericType && (descriptor.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))) ? descriptor.PropertyType.GetGenericArguments()[0] : descriptor.PropertyType;
                        table.Columns.Add(descriptor.Name, type);
                    }
                }
            }
            foreach (T local in base.Items)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor descriptor2 in properties)
                {
                    if (table.Columns.Contains(descriptor2.Name))
                    {
                        object obj2 = descriptor2.GetValue(local);
                        row[descriptor2.Name] = (obj2 == null) ? DBNull.Value : obj2;
                    }
                }
                table.Rows.Add(row);
            }
            if (!flag)
            {
                dataset.Tables.Add(table);
            }
            if ((parentKeys != null) && !flag)
            {
                bool flag2 = false;
                DataTable table2 = dataset.Tables[dataset.Tables.Count - 1];
                Trace.WriteLine(table2.TableName + " - Found");
                List<DataColumn> list3 = new List<DataColumn>();
                if (table2 != null)
                {
                    foreach (DataColumn column in parentKeys)
                    {
                        if (!table2.Columns.Contains(column.ColumnName))
                        {
                            flag2 = true;
                        }
                        Trace.WriteLine(table2.TableName + " - Skip " + flag2);
                        list3.Add(table2.Columns[column.ColumnName]);
                    }
                    if (!flag2)
                    {
                        Trace.WriteLine(table2.TableName + " - relation added ");
                        int num = parentKeys.GetHashCode() + list3.GetHashCode();
                        if (!dataset.Relations.Contains(num.ToString()))
                        {
                            dataset.Relations.Add(num.ToString(), parentKeys.ToArray(), list3.ToArray()).Nested = true;
                        }
                    }
                }
            }
            if (includeChildren)
            {
                foreach (PropertyDescriptor descriptor3 in list)
                {
                    foreach (T local2 in base.Items)
                    {
                        object[] parameters = new object[] { dataset, list2, includeChildren };
                        MethodInfo method = descriptor3.PropertyType.GetMethod("AddRelations", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (method == null)
                        {
                            throw new InvalidOperationException("The template method for converting a TList to a Dataset has been altered. Can not include child collections.");
                        }
                        object obj3 = null;
                        obj3 = method.Invoke(Convert.ChangeType(descriptor3.GetValue(local2), descriptor3.PropertyType), parameters);
                        if (obj3 == null)
                        {
                            throw new ArgumentException("Could not successfully convert nested child relationships to a dataset, consider a shallow conversion.");
                        }
                        dataset = (DataSet) obj3;
                    }
                }
            }
            return dataset;
        }

        public void ApplyFilter()
        {
            for (int i = 0; i < this.excludedItems.Count; i++)
            {
                base.Add(this.excludedItems[i]);
            }
            this.excludedItems.Clear();
            if ((this._filterString != null) & (this._filterString.Length > 0))
            {
                Filter<ListBase<T>, T> filter = new Filter<ListBase<T>, T>((ListBase<T>) this, this._filterString);
            }
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0, 0));
        }

        public void ApplyFilter(Predicate<T> match)
        {
            int index;
            this._filterString = string.Empty;
            for (index = 0; index < this.excludedItems.Count; index++)
            {
                base.Add(this.excludedItems[index]);
            }
            this.excludedItems.Clear();
            for (index = base.Items.Count - 1; index >= 0; index--)
            {
                if (!match(base.Items[index]))
                {
                    this.RemoveFilteredItem(index);
                }
            }
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0, 0));
        }

        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            this._sortProperty = null;
            this._sortDescriptions = sorts;
            SortComparer<T> comparer = new SortComparer<T>(sorts);
            this.ApplySortInternal(comparer);
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            this.ApplySortCore(property, direction);
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            this._sortDirection = direction;
            this._sortProperty = prop;
            SortComparer<T> comparer = new SortComparer<T>(prop, direction);
            this.ApplySortInternal(comparer);
        }

        private void ApplySortInternal(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }
            this.ApplySortInternal(new Comparison<T>(comparer.Compare));
        }

        private void ApplySortInternal(Comparison<T> comparison)
        {
            if (comparison == null)
            {
                throw new ArgumentNullException("The comparison parameter must be a valid object instance.");
            }
            if (this._OriginalList.Count == 0)
            {
                this._OriginalList.AddRange(this);
            }
            List<T> list = base.Items as List<T>;
            if (list != null)
            {
                list.Sort(comparison);
                this._isSorted = true;
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        public virtual object Clone()
        {
            throw new NotImplementedException("Method not implemented.");
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                EventHandler disposed = this.Disposed;
                if (disposed != null)
                {
                    disposed(this, EventArgs.Empty);
                }
            }
        }

        internal void EntityChanged(T entity)
        {
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, base.IndexOf(entity)));
        }

        public T Find(Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }
            foreach (T local in base.Items)
            {
                if (match(local))
                {
                    return local;
                }
            }
            return default(T);
        }

        public virtual T Find(Enum column, object value)
        {
            return this.Find(column.ToString(), value, true);
        }

        public virtual T Find(string propertyName, object value)
        {
            return this.Find(propertyName, value, true);
        }

        public virtual T Find(Enum column, object value, bool ignoreCase)
        {
            return this.Find(column.ToString(), value, ignoreCase);
        }

        public virtual T Find(string propertyName, object value, bool ignoreCase)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T)).Find(propertyName, true);
            if (prop != null)
            {
                int num = this.FindCore(prop, value, ignoreCase);
                if (num > -1)
                {
                    return base[num];
                }
                return default(T);
            }
            return default(T);
        }

        public ListBase<T> FindAll(Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }
            ListBase<T> base2 = this.Clone() as ListBase<T>;
            base2.ClearItems();
            foreach (T local in base.Items)
            {
                if (match(local))
                {
                    base2.Add(local);
                }
            }
            return base2;
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            return this.FindCore(prop, key, 0, true);
        }

        protected virtual int FindCore(PropertyDescriptor prop, object key, bool ignoreCase)
        {
            return this.FindCore(prop, key, 0, ignoreCase);
        }

        protected virtual int FindCore(PropertyDescriptor prop, object key, int start, bool ignoreCase)
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
                    if (string.Compare(obj2.ToString(), key.ToString(), ignoreCase) == 0)
                    {
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

        public void ForEach(Action<T> action)
        {
            foreach (T local in base.Items)
            {
                action(local);
            }
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if ((listAccessors == null) || (listAccessors.Length == 0))
            {
                return this._propertyCollection;
            }
            if (this._childCollectionProperties == null)
            {
                this._childCollectionProperties = new Dictionary<string, PropertyDescriptorCollection>();
            }
            string key = listAccessors[0].PropertyType.FullName;
            if (this._childCollectionProperties.ContainsKey(key))
            {
                return this._childCollectionProperties[key];
            }
            PropertyDescriptorCollection bindableProperties = EntityHelper.GetBindableProperties(listAccessors[0].PropertyType);
            this._childCollectionProperties.Add(key, bindableProperties);
            return bindableProperties;
        }

        public IList GetList()
        {
            return this;
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return this._listName;
        }

        private void InitializeList()
        {
            this._propertyCollection = EntityHelper.GetBindableProperties(typeof(T));
            this._listName = typeof(T).Name;
        }

        public static bool IsNullOrEmpty(ListBase<T> list)
        {
            return ((list == null) || (list.Count == 0));
        }

        public static object MakeCopyOf(object x)
        {
            if (x is ICloneable)
            {
                return ((ICloneable) x).Clone();
            }
            throw new NotSupportedException("object not cloneable");
        }

        public void OnDeserialization(object sender)
        {
            this.InitializeList();
        }

        private void OnItemChanged(object sender, EventArgs args)
        {
            int newIndex = base.Items.IndexOf((T) sender);
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, newIndex));
        }

        public void RemoveFilter()
        {
            this.Filter = string.Empty;
        }

        internal void RemoveFilteredItem(int index)
        {
            T item = base.Items[index];
            if (item != null)
            {
                this.ExcludedItems.Add(item);
                base.RemoveItem(index);
            }
        }

        protected override void RemoveSortCore()
        {
            if (this._isSorted)
            {
                base.Clear();
                foreach (T local in this._OriginalList)
                {
                    base.Add(local);
                }
                this._OriginalList.Clear();
                this._sortProperty = null;
                this._sortDescriptions = null;
                this._isSorted = false;
            }
        }

        public virtual void Shuffle()
        {
            if (this._OriginalList.Count == 0)
            {
                this._OriginalList.AddRange(this);
            }
            Random random = new Random();
            for (int i = base.Count - 1; i > 0; i--)
            {
                int num2 = random.Next(i + 1);
                T local = base[i];
                base[i] = base[num2];
                base[num2] = local;
            }
        }

        public void Sort(IComparer<T> comparer)
        {
            this.ApplySortInternal(comparer);
        }

        public void Sort(Comparison<T> comparison)
        {
            this.ApplySortInternal(comparison);
        }

        public void Sort(string orderBy)
        {
            SortComparer<T> comparer = new SortComparer<T>(orderBy);
            this.ApplySortInternal(new Comparison<T>(comparer.Compare));
        }

        public T[] ToArray()
        {
            List<T> list = new List<T>(base.Items.Count);
            foreach (T local in base.Items)
            {
                list.Add(local);
            }
            return list.ToArray();
        }

        public DataSet ToDataSet(bool includeChildren)
        {
            DataSet dataset = new DataSet();
            return this.AddRelations(dataset, null, includeChildren);
        }

        public override string ToString()
        {
            string text = base.GetType().Name + " Collection" + Environment.NewLine;
            foreach (T local in Items)
            {
                text = text + local.ToString() + Environment.NewLine;
            }
            return text;
        }

        public bool ContainsListCollection
        {
            get
            {
                return this._containsListCollection;
            }
            set
            {
                this._containsListCollection = value;
            }
        }

        public List<T> ExcludedItems
        {
            get
            {
                return this.excludedItems;
            }
        }

        public string Filter
        {
            get
            {
                return this._filterString;
            }
            set
            {
                if (value != this._filterString)
                {
                    this._filterString = value;
                    this.ApplyFilter();
                }
            }
        }

        public bool IsFiltering
        {
            get
            {
                return (this.excludedItems.Count > 0);
            }
        }

        protected override bool IsSortedCore
        {
            get
            {
                return this._isSorted;
            }
        }

        protected virtual PropertyDescriptorCollection PropertyCollection
        {
            get
            {
                return this._propertyCollection;
            }
            set
            {
                this._propertyCollection = value;
            }
        }

        public ISite Site
        {
            get
            {
                return this._site;
            }
            set
            {
                this._site = value;
            }
        }

        public ListSortDescriptionCollection SortDescriptions
        {
            get
            {
                return this._sortDescriptions;
            }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return this._sortDirection;
            }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return this._sortProperty;
            }
        }

        public bool SupportsAdvancedSorting
        {
            get
            {
                return true;
            }
        }

        public bool SupportsFiltering
        {
            get
            {
                return true;
            }
        }

        protected override bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }

        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        bool IRaiseItemChangedEvents.RaisesItemChangedEvents
        {
            get
            {
                return true;
            }
        }
    }
}

