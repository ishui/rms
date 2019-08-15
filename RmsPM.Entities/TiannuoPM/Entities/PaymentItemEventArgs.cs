namespace TiannuoPM.Entities
{
    using System;

    public class PaymentItemEventArgs : EventArgs
    {
        private PaymentItemColumn column;
        private object value;

        public PaymentItemEventArgs(PaymentItemColumn column)
        {
            this.column = column;
        }

        public PaymentItemEventArgs(PaymentItemColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public PaymentItemColumn Column
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

