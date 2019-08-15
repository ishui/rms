namespace TiannuoPM.Entities
{
    using System;

    public class SystemUserEventArgs : EventArgs
    {
        private SystemUserColumn column;
        private object value;

        public SystemUserEventArgs(SystemUserColumn column)
        {
            this.column = column;
        }

        public SystemUserEventArgs(SystemUserColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public SystemUserColumn Column
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

