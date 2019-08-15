namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using System;

    [Serializable]
    internal class CommentKey : KeyBase
    {
        private DateTime dateAdded;
        private string name;

        public CommentKey(int id, string name, DateTime dateAdded) : base(id)
        {
            this.name = name;
            this.dateAdded = dateAdded;
        }

        public DateTime DateAdded
        {
            get
            {
                return this.dateAdded;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }
    }
}

