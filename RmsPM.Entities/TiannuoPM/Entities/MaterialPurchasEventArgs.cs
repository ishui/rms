namespace TiannuoPM.Entities
{
    using System;

    public class MaterialPurchasEventArgs : EventArgs
    {
        private MaterialPurchasColumn column;
        private object value;

        public MaterialPurchasEventArgs(MaterialPurchasColumn column)
        {
            this.column = column;
        }

        public MaterialPurchasEventArgs(MaterialPurchasColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public MaterialPurchasColumn Column
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

