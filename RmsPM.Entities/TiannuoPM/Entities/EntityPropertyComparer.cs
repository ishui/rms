namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    public class EntityPropertyComparer : IComparer
    {
        private string PropertyName;

        public EntityPropertyComparer(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public int Compare(object x, object y)
        {
            object obj2 = x.GetType().GetProperty(this.PropertyName).GetValue(x, null);
            object obj3 = y.GetType().GetProperty(this.PropertyName).GetValue(y, null);
            if ((obj2 != null) && (obj3 == null))
            {
                return 1;
            }
            if ((obj2 == null) && (obj3 != null))
            {
                return -1;
            }
            if ((obj2 == null) && (obj3 == null))
            {
                return 0;
            }
            return ((IComparable) obj2).CompareTo(obj3);
        }
    }
}

