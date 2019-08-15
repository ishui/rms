namespace Codefresh.PhotoBrowserLibrary
{
    using Codefresh.PhotoBrowserLibrary.Collections;
    using Codefresh.PhotoBrowserLibrary.DataAccessLayer;
    using System;
    using System.IO;

    [Serializable]
    public class Photo : PhotoObjectBase
    {
        private bool beDeleted;
        private Comments comments;
        private DateTime dateTaken;
        private long filesize;
        private string name;
        private int viewed;
        private string virtualPath;

        internal Photo(SessionToken token, string name, string virtualPath, DateTime dateTaken, long filesize) : base(token)
        {
            this.beDeleted = false;
            this.name = name;
            this.virtualPath = virtualPath;
            this.dateTaken = dateTaken;
            this.filesize = filesize;
            ThumbnailUtilities.CreateThumbnail(token.MapPath(this.FullVirtualPath));
        }

        internal Photo(SessionToken token, int id, string name, string virtualPath, DateTime dateTaken, long filesize, int viewed) : base(token)
        {
            this.beDeleted = false;
            this.name = name;
            this.virtualPath = virtualPath;
            base.SetId(id);
            this.dateTaken = dateTaken;
            this.filesize = filesize;
            this.viewed = viewed;
        }

        public void AddComment(Comment comment)
        {
            new CommentDB(base.token.DBConnection).Insert(this, comment);
            this.comments.Add(comment);
        }

        public Comments GetComments()
        {
            if (this.comments == null)
            {
                this.comments = new CommentDB(base.token.DBConnection).GetComments(base.token, this);
            }
            return this.comments;
        }

        public void IncrementViewed()
        {
            this.viewed++;
            new PhotoDB(base.token.DBConnection).IncrementViewedCount(this);
        }

        public bool BeDeleted
        {
            get
            {
                return this.beDeleted;
            }
            set
            {
                this.beDeleted = value;
            }
        }

        public DateTime DateTaken
        {
            get
            {
                return this.dateTaken;
            }
        }

        public long FileSize
        {
            get
            {
                return this.filesize;
            }
        }

        public string FileSizeText
        {
            get
            {
                long num = this.filesize / ((long) 0x400);
                if (num >= 1)
                {
                    return (num + " KB");
                }
                return (this.filesize + " b");
            }
        }

        public string FullThumbnailVirtualPath
        {
            get
            {
                string virtualPath = string.Concat(new object[] { this.VirtualPath, Path.DirectorySeparatorChar, ThumbnailUtilities.ThumbnailDirectory, Path.DirectorySeparatorChar, this.name });
                if (!(File.Exists(base.token.MapPath(virtualPath)) || this.beDeleted))
                {
                    ThumbnailUtilities.CreateThumbnail(base.token.MapPath(this.FullVirtualPath));
                }
                return virtualPath;
            }
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

        public int Viewed
        {
            get
            {
                return this.viewed;
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

