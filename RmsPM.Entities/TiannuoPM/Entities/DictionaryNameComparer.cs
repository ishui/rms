namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class DictionaryNameComparer : IComparer<DictionaryName>
    {
        private DictionaryNameColumn whichComparison;

        public DictionaryNameComparer()
        {
        }

        public DictionaryNameComparer(DictionaryNameColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(DictionaryName a, DictionaryName b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(DictionaryName a, DictionaryName b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(DictionaryName entity)
        {
            return entity.GetHashCode();
        }

        public DictionaryNameColumn WhichComparison
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

