namespace TiannuoPM.Entities
{
    using System;

    public class DictionaryItemEventArgs : EventArgs
    {
        private DictionaryItemColumn column;
        private object value;

        public DictionaryItemEventArgs(DictionaryItemColumn column)
        {
            this.column = column;
        }

        public DictionaryItemEventArgs(DictionaryItemColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public DictionaryItemColumn Column
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

