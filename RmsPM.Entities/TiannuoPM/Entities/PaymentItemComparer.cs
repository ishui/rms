namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class PaymentItemComparer : IComparer<PaymentItem>
    {
        private PaymentItemColumn whichComparison;

        public PaymentItemComparer()
        {
        }

        public PaymentItemComparer(PaymentItemColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(PaymentItem a, PaymentItem b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(PaymentItem a, PaymentItem b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(PaymentItem entity)
        {
            return entity.GetHashCode();
        }

        public PaymentItemColumn WhichComparison
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

