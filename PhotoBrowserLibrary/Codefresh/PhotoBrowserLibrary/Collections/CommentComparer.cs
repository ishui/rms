namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using System;
    using System.Collections;

    public class CommentComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            CommentKey key = (CommentKey) x;
            CommentKey key2 = (CommentKey) y;
            int num = key.DateAdded.CompareTo(key2.DateAdded);
            if (num == 0)
            {
                return key.Id.CompareTo(key2.Id);
            }
            return num;
        }
    }
}

