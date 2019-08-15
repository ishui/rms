namespace Codefresh.PhotoBrowserLibrary
{
    using Codefresh.PhotoBrowserLibrary.Collections;
    using System;
    using System.IO;

    [Serializable]
    public class PhotoDirectory : PhotoObjectBase
    {
        private PhotoDirectories dirs;
        private string name;
        private Photos photos;
        private string virtualPath;

        internal PhotoDirectory(SessionToken token, string name, string virtualPath) : base(token)
        {
            this.name = name;
            this.virtualPath = virtualPath;
        }

        internal PhotoDirectory(SessionToken token, int id, string name, string virtualPath) : this(token, name, virtualPath)
        {
            base.SetId(id);
        }

        public PhotoDirectories GetDirectories()
        {
            if (this.dirs == null)
            {
                this.dirs = DirectoryUtilities.GetDirectories(this, base.token, base.token.MapPath(this.FullVirtualPath));
            }
            return this.dirs;
        }

        public Photos GetPhotos()
        {
            this.photos = PhotoUtilities.GetPhotos(base.token, this);
            return this.photos;
        }

        public string FullVirtualPath
        {
            get
            {
                return (this.VirtualPath + Path.DirectorySeparatorChar + this.Name);
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string VirtualPath
        {
            get
            {
                return this.virtualPath;
            }
        }
    }
}

