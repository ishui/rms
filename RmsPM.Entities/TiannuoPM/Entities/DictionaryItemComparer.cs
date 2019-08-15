namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class DictionaryItemComparer : IComparer<DictionaryItem>
    {
        private DictionaryItemColumn whichComparison;

        public DictionaryItemComparer()
        {
        }

        public DictionaryItemComparer(DictionaryItemColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(DictionaryItem a, DictionaryItem b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(DictionaryItem a, DictionaryItem b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(DictionaryItem entity)
        {
            return entity.GetHashCode();
        }

        public DictionaryItemColumn WhichComparison
        {
            get
            {
                return this.whichComparison;
            }
            set
            {
                this.whichComparison = value;
            }
        }
    }
}

