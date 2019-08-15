namespace Codefresh.PhotoBrowserLibrary
{
    using System;
    using System.Data;

    [Serializable]
    public class SessionToken
    {
        [NonSerialized]
        private IDbConnection conn;
        private string photosRootFullPath;

        public SessionToken(string photosRootFullPath, IDbConnection connection)
        {
            this.photosRootFullPath = photosRootFullPath;
            this.conn = connection;
        }

        public string MapPath(string virtualPath)
        {
            return (this.photosRootFullPath + virtualPath);
        }

        public IDbConnection DBConnection
        {
            get
            {
                return this.conn;
            }
        }

        public string PhotosRootFullPath
        {
            get
            {
                return this.photosRootFullPath;
            }
        }
    }
}

