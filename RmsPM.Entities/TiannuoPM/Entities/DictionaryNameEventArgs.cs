namespace TiannuoPM.Entities
{
    using System;

    public class DictionaryNameEventArgs : EventArgs
    {
        private DictionaryNameColumn column;
        private object value;

        public DictionaryNameEventArgs(DictionaryNameColumn column)
        {
            this.column = column;
        }

        public DictionaryNameEventArgs(DictionaryNameColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public DictionaryNameColumn Column
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

