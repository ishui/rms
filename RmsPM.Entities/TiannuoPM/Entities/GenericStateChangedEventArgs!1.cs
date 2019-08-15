namespace TiannuoPM.Entities
{
    using System;

    public class GenericStateChangedEventArgs<T> : EventArgs
    {
        private T newValue;
        private T oldValue;

        public GenericStateChangedEventArgs(T oldValue, T newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public T NewValue
        {
            get
            {
                return this.newValue;
            }
        }

        public T OldValue
        {
            get
            {
                return this.oldValue;
            }
        }
    }
}

