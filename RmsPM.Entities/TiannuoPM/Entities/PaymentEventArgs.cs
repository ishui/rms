namespace TiannuoPM.Entities
{
    using System;

    public class PaymentEventArgs : EventArgs
    {
        private PaymentColumn column;
        private object value;

        public PaymentEventArgs(PaymentColumn column)
        {
            this.column = column;
        }

        public PaymentEventArgs(PaymentColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public PaymentColumn Column
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

