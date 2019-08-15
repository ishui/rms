namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using System;

    [Serializable]
    public abstract class KeyBase
    {
        private int id;

        public KeyBase(int id)
        {
            this.id = id;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }
    }
}

