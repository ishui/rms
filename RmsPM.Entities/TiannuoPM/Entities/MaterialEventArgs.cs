namespace TiannuoPM.Entities
{
    using System;

    public class MaterialEventArgs : EventArgs
    {
        private MaterialColumn column;
        private object value;

        public MaterialEventArgs(MaterialColumn column)
        {
            this.column = column;
        }

        public MaterialEventArgs(MaterialColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public MaterialColumn Column
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

