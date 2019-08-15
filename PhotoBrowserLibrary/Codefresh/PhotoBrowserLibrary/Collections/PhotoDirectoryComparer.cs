namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using System;
    using System.Collections;

    [Serializable]
    public class PhotoDirectoryComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            PhotoDirectoryKey key = (PhotoDirectoryKey) x;
            PhotoDirectoryKey key2 = (PhotoDirectoryKey) y;
            return key.Name.CompareTo(key2.Name);
        }
    }
}

