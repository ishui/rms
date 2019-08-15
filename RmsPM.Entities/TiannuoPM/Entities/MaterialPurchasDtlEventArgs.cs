namespace TiannuoPM.Entities
{
    using System;

    public class MaterialPurchasDtlEventArgs : EventArgs
    {
        private MaterialPurchasDtlColumn column;
        private object value;

        public MaterialPurchasDtlEventArgs(MaterialPurchasDtlColumn column)
        {
            this.column = column;
        }

        public MaterialPurchasDtlEventArgs(MaterialPurchasDtlColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public MaterialPurchasDtlColumn Column
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

