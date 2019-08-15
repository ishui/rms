namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class TroubleComparer : IComparer<Trouble>
    {
        private TroubleColumn whichComparison;

        public TroubleComparer()
        {
        }

        public TroubleComparer(TroubleColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(Trouble a, Trouble b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(Trouble a, Trouble b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(Trouble entity)
        {
            return entity.GetHashCode();
        }

        public TroubleColumn WhichComparison
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

