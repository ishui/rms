namespace Codefresh.PhotoBrowserLibrary.Collections
{
    using Codefresh.PhotoBrowserLibrary;
    using System;
    using System.Collections;

    [Serializable]
    public class PhotoDirectories : PhotoCollectionBase
    {
        public void Add(PhotoDirectory photoDirectory)
        {
            KeyBase key = this.CreateKey(photoDirectory);
            base.Add(key, photoDirectory);
        }

        public bool Contains(string fullVirtualPath)
        {
            IEnumerator enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                PhotoDirectory directory = (PhotoDirectory)enumerator.Current;
                if (directory.FullVirtualPath.Equals(fullVirtualPath))
                {
                    return true;
                }
            }
            return false;
        }

        protected override IComparer CreateComparer()
        {
            return new PhotoDirectoryComparer();
        }

        protected override KeyBase CreateKey(PhotoObjectBase obj)
        {
            PhotoDirectory directory = (PhotoDirectory) obj;
            return new PhotoDirectoryKey(directory.Id, directory.Name);
        }

        public void Remove(PhotoDirectory photoDirectory)
        {
            base.Remove(this.CreateKey(photoDirectory));
        }

        public void Remove(string fullVirtualPath)
        {
            IEnumerator enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                PhotoDirectory directory = (PhotoDirectory)enumerator.Current;
                if (directory.FullVirtualPath.Equals(fullVirtualPath))
                {
                    this.Remove(directory);
                    break;
                }
            }
        }
    }
}

