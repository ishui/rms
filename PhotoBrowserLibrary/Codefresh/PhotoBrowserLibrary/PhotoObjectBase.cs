namespace Codefresh.PhotoBrowserLibrary
{
    using System;

    [Serializable]
    public abstract class PhotoObjectBase
    {
        private int id;
        protected SessionToken token;

        protected PhotoObjectBase()
        {
            this.id = -1;
        }

        protected PhotoObjectBase(SessionToken token) : this()
        {
            this.token = token;
        }

        internal void SetId(int id)
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

