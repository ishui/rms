namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class PayoutItemComparer : IComparer<PayoutItem>
    {
        private PayoutItemColumn whichComparison;

        public PayoutItemComparer()
        {
        }

        public PayoutItemComparer(PayoutItemColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(PayoutItem a, PayoutItem b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(PayoutItem a, PayoutItem b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(PayoutItem entity)
        {
            return entity.GetHashCode();
        }

        public PayoutItemColumn WhichComparison
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

