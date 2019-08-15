namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using System;

    [Serializable]
    internal class PhotoDirectoryKey : KeyBase
    {
        private string name;

        public PhotoDirectoryKey(int id, string name) : base(id)
        {
            this.name = name;
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

