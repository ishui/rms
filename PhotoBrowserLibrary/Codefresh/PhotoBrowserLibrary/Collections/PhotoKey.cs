namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using System;

    [Serializable]
    internal class PhotoKey : KeyBase
    {
        public DateTime dateTaken;
        public string name;

        public PhotoKey(int id, DateTime dateTaken, string name) : base(id)
        {
            this.dateTaken = dateTaken;
            this.name = name;
        }

        public DateTime DateTaken
        {
            get
            {
                return this.dateTaken;
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

