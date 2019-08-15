namespace TiannuoPM.Entities
{
    using System;

    public class PayoutItemEventArgs : EventArgs
    {
        private PayoutItemColumn column;
        private object value;

        public PayoutItemEventArgs(PayoutItemColumn column)
        {
            this.column = column;
        }

        public PayoutItemEventArgs(PayoutItemColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public PayoutItemColumn Column
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

