namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class SortComparer<T> : IComparer<T>
    {
        private ListSortDirection m_Direction;
        private PropertyDescriptor m_PropDesc;
        private PropertyDescriptorCollection m_PropertyDescriptors;
        private ListSortDescriptionCollection m_SortCollection;

        public SortComparer(ListSortDescriptionCollection sortCollection)
        {
            this.m_SortCollection = null;
            this.m_PropDesc = null;
            this.m_Direction = ListSortDirection.Ascending;
            this.m_PropertyDescriptors = null;
            this.Initialize();
            this.m_SortCollection = sortCollection;
        }

        public SortComparer(string orderBy)
        {
            this.m_SortCollection = null;
            this.m_PropDesc = null;
            this.m_Direction = ListSortDirection.Ascending;
            this.m_PropertyDescriptors = null;
            this.Initialize();
            this.m_SortCollection = this.ParseOrderBy(orderBy);
        }

        public SortComparer(PropertyDescriptor propDesc, ListSortDirection direction)
        {
            this.m_SortCollection = null;
            this.m_PropDesc = null;
            this.m_Direction = ListSortDirection.Ascending;
            this.m_PropertyDescriptors = null;
            this.Initialize();
            this.m_PropDesc = propDesc;
            this.m_Direction = direction;
        }

        public int Compare(T x, T y)
        {
            if (this.m_PropDesc != null)
            {
                object xValue = this.m_PropDesc.GetValue(x);
                object yValue = this.m_PropDesc.GetValue(y);
                return this.CompareValues(xValue, yValue, this.m_Direction);
            }
            if ((this.m_SortCollection != null) && (this.m_SortCollection.Count > 0))
            {
                return this.RecursiveCompareInternal(x, y, 0);
            }
            return 0;
        }

        private int CompareValues(object xValue, object yValue, ListSortDirection direction)
        {
            int num = 0;
            if ((xValue != null) && (yValue == null))
            {
                num = 1;
            }
            else if ((xValue == null) && (yValue != null))
            {
                num = -1;
            }
            else if ((xValue == null) && (yValue == null))
            {
                num = 0;
            }
            else if (xValue is IComparable)
            {
                num = ((IComparable) xValue).CompareTo(yValue);
            }
            else if (yValue is IComparable)
            {
                num = ((IComparable) yValue).CompareTo(xValue);
            }
            else if (!xValue.Equals(yValue))
            {
                num = xValue.ToString().CompareTo(yValue.ToString());
            }
            if (direction == ListSortDirection.Ascending)
            {
                return num;
            }
            return (num * -1);
        }

        private void Initialize()
        {
            Type type = typeof(T);
            if (!type.IsPublic)
            {
                throw new ArgumentException(string.Format("Type \"{0}\" is not public.", typeof(T).FullName));
            }
            this.m_PropertyDescriptors = TypeDescriptor.GetProperties(typeof(T));
        }

        private ListSortDescriptionCollection ParseOrderBy(string orderBy)
        {
            if ((orderBy == null) || (orderBy.Length == 0))
            {
                throw new ArgumentNullException("orderBy");
            }
            string[] textArray = orderBy.Split(new char[] { ',' });
            ListSortDescription[] sorts = new ListSortDescription[textArray.Length];
            ListSortDirection ascending = ListSortDirection.Ascending;
            for (int i = 0; i < textArray.Length; i++)
            {
                ascending = ListSortDirection.Ascending;
                string text = textArray[i].Trim();
                if (text.ToUpper().EndsWith(" DESC"))
                {
                    ascending = ListSortDirection.Descending;
                    text = text.Substring(0, text.ToUpper().LastIndexOf(" DESC"));
                }
                else if (text.ToUpper().EndsWith(" ASC"))
                {
                    text = text.Substring(0, text.ToUpper().LastIndexOf(" ASC"));
                }
                text = text.Trim();
                PropertyDescriptor property = this.m_PropertyDescriptors[text];
                if (property == null)
                {
                    throw new ArgumentException(string.Format("The property \"{0}\" is not a valid property.", text));
                }
                sorts[i] = new ListSortDescription(property, ascending);
            }
            return new ListSortDescriptionCollection(sorts);
        }

        private int RecursiveCompareInternal(T x, T y, int index)
        {
            if (index >= this.m_SortCollection.Count)
            {
                return 0;
            }
            ListSortDescription description = this.m_SortCollection[index];
            object xValue = description.PropertyDescriptor.GetValue(x);
            object yValue = description.PropertyDescriptor.GetValue(y);
            int num = this.CompareValues(xValue, yValue, description.SortDirection);
            if (num == 0)
            {
                return this.RecursiveCompareInternal(x, y, ++index);
            }
            return num;
        }
    }
}

