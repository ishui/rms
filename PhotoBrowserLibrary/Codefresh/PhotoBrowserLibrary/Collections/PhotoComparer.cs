namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using System;
    using System.Collections;

    [Serializable]
    public class PhotoComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            PhotoKey key = (PhotoKey) x;
            PhotoKey key2 = (PhotoKey) y;
            int num = key.DateTaken.CompareTo(key2.DateTaken);
            if (num == 0)
            {
                return key.Name.CompareTo(key2.Name);
            }
            return num;
        }
    }
}

