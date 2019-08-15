namespace Codefresh.PhotoBrowserLibrary
{
    using Codefresh.PhotoBrowserLibrary.Collections;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;

    public class DirectoryBrowser
    {
        private string _path;
        private const string DATABASE_NAME = "PhotosServer.mdb";
        private PhotoDirectories dirs;
        private PhotoDirectories rootdir;
        private SessionToken token;

        public DirectoryBrowser(string mappedPhotosDbPath, string path)
        {
            this._path = path;
            this.token = new SessionToken(mappedPhotosDbPath, this.OpenDatabaseConnection(mappedPhotosDbPath));
        }

        public bool CreateRootDirectorie(string projcode, string name)
        {
            return DirectoryUtilities.CreateRootDirectorie(this.token, projcode, name);
        }

        public void DeletePhoto(Photo photo, string rootpath)
        {
            PhotoUtilities.DeletePhoto(this.token, photo, rootpath);
        }

        public PhotoDirectories GetDirectories()
        {
            if (this.dirs == null)
            {
                this.dirs = DirectoryUtilities.GetDirectorie((PhotoDirectory) this.rootdir[0], this.token, this.token.PhotosRootFullPath + this._path);
            }
            return this.dirs;
        }

        public string GetRootPath(string projcode)
        {
            return DirectoryUtilities.GetRootDirectorie(this.token, projcode);
        }

        private IDbConnection OpenDatabaseConnection(string photosPhysicalPath)
        {
            string text = photosPhysicalPath + Path.DirectorySeparatorChar + "PhotosServer.mdb";
            IDbConnection connection = new OleDbConnection("PROVIDER=MICROSOFT.JET.OLEDB.4.0;DATA SOURCE=" + text);
            connection.Open();
            return connection;
        }

        public bool RootPathExist(string name)
        {
            return DirectoryUtilities.RootDirectorieExist(this.token, name);
        }

        public PhotoDirectories rootDir
        {
            get
            {
                if (this.rootdir == null)
                {
                    PhotoDirectory parent = new PhotoDirectory(this.token, this._path, "");
                    this.rootdir = DirectoryUtilities.GetDirectorie(parent, this.token, this.token.PhotosRootFullPath + @"\" + this._path);
                }
                return this.rootdir;
            }
        }
    }
}

