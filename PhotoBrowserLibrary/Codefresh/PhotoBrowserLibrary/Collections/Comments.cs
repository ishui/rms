namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using Codefresh.PhotoBrowserLibrary;
    using System;
    using System.Collections;

    [Serializable]
    public class Comments : PhotoCollectionBase
    {
        public void Add(Comment comment)
        {
            KeyBase key = this.CreateKey(comment);
            base.Add(key, comment);
        }

        protected override IComparer CreateComparer()
        {
            return new CommentComparer();
        }

        protected override KeyBase CreateKey(PhotoObjectBase obj)
        {
            Comment comment = (Comment) obj;
            return new CommentKey(comment.Id, comment.Name, comment.DateAdded);
        }

        public void Remove(Comment comment)
        {
            base.Remove(this.CreateKey(comment));
        }
    }
}

