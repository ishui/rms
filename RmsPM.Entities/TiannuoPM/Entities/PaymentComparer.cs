namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class PaymentComparer : IComparer<Payment>
    {
        private PaymentColumn whichComparison;

        public PaymentComparer()
        {
        }

        public PaymentComparer(PaymentColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(Payment a, Payment b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(Payment a, Payment b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(Payment entity)
        {
            return entity.GetHashCode();
        }

        public PaymentColumn WhichComparison
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

