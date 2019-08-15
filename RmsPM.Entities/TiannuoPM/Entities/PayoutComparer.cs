namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class PayoutComparer : IComparer<Payout>
    {
        private PayoutColumn whichComparison;

        public PayoutComparer()
        {
        }

        public PayoutComparer(PayoutColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(Payout a, Payout b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(Payout a, Payout b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(Payout entity)
        {
            return entity.GetHashCode();
        }

        public PayoutColumn WhichComparison
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

