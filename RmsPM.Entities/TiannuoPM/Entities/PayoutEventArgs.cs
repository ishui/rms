namespace TiannuoPM.Entities
{
    using System;

    public class PayoutEventArgs : EventArgs
    {
        private PayoutColumn column;
        private object value;

        public PayoutEventArgs(PayoutColumn column)
        {
            this.column = column;
        }

        public PayoutEventArgs(PayoutColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public PayoutColumn Column
        {
            get
            {
                return this.column;
            }
        }

        public object Value
        {
            get
            {
                return this.value;
            }
        }
    }
}

