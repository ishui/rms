namespace TiannuoPM.Entities
{
    using System;

    public class TroubleEventArgs : EventArgs
    {
        private TroubleColumn column;
        private object value;

        public TroubleEventArgs(TroubleColumn column)
        {
            this.column = column;
        }

        public TroubleEventArgs(TroubleColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public TroubleColumn Column
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

