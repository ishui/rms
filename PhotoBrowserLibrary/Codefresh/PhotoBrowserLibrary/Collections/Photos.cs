namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using Codefresh.PhotoBrowserLibrary;
    using System;
    using System.Collections;

    [Serializable]
    public class Photos : PhotoCollectionBase
    {
        public void Add(Photo photo)
        {
            KeyBase key = this.CreateKey(photo);
            base.Add(key, photo);
        }

        public bool Contains(string fullVirtualPath)
        {
            IEnumerator enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                Photo photo = (Photo)enumerator.Current;
                if (photo.FullVirtualPath.Equals(fullVirtualPath))
                {
                    return true;
                }
            }
            return false;
        }

        protected override IComparer CreateComparer()
        {
            return new PhotoComparer();
        }

        protected override KeyBase CreateKey(PhotoObjectBase obj)
        {
            Photo photo = (Photo) obj;
            return new PhotoKey(photo.Id, photo.DateTaken, photo.Name);
        }

        public void Remove(Photo photo)
        {
            base.Remove(this.CreateKey(photo));
        }

        public void Remove(string fullVirtualPath)
        {
            IEnumerator enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                Photo photo = (Photo)enumerator.Current;
                if (photo.FullVirtualPath.Equals(fullVirtualPath))
                {
                    this.Remove(photo);
                    break;
                }
            }
        }
    }
}

