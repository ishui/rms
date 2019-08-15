namespace Codefresh.PhotoBrowserLibrary
{
    using Codefresh.PhotoBrowserLibrary.Collections;
    using Codefresh.PhotoBrowserLibrary.DataAccessLayer;
    using System;
    using System.IO;

    public sealed class DirectoryUtilities
    {
        private DirectoryUtilities()
        {
        }

        public static bool CreateRootDirectorie(SessionToken token, string projcode, string name)
        {
            PhotoDirectoryDB ydb = new PhotoDirectoryDB(token.DBConnection);
            bool flag = ydb.CreateRootDirectorie(token, projcode, name);
            if (flag)
            {
                ydb.Insert(null, new PhotoDirectory(token, name, ""));
                Directory.CreateDirectory(token.PhotosRootFullPath + @"\" + name);
            }
            return flag;
        }

        public static PhotoDirectories GetDirectorie(PhotoDirectory parent, SessionToken token, string path)
        {
            PhotoDirectoryDB ydb = new PhotoDirectoryDB(token.DBConnection);
            if (!Directory.Exists(path))
            {
                path = path.Substring(token.PhotosRootFullPath.Length, path.Length - token.PhotosRootFullPath.Length);
                string name = path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                string virtualPath = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
                ydb.Delete(name, virtualPath);
                return new PhotoDirectories();
            }
            PhotoDirectories directories = ydb.GetDirectorie(token, "", parent.Name);
            PhotoDirectories directories2 = (PhotoDirectories) directories.Clone();
            DirectoryInfo[] infoArray = new DirectoryInfo(path).GetDirectories();
            return directories;
        }

        public static PhotoDirectories GetDirectories(PhotoDirectory parent, SessionToken token, string path)
        {
            string virtualPath;
            PhotoDirectoryDB ydb = new PhotoDirectoryDB(token.DBConnection);
            if (!Directory.Exists(path))
            {
                path = path.Substring(token.PhotosRootFullPath.Length, path.Length - token.PhotosRootFullPath.Length);
                string name = path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                virtualPath = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
                ydb.Delete(name, virtualPath);
                return new PhotoDirectories();
            }
            PhotoDirectories directories = ydb.GetDirectories(token, GetVirtualPath(token, path));
            PhotoDirectories directories2 = (PhotoDirectories) directories.Clone();
            DirectoryInfo[] infoArray = new DirectoryInfo(path).GetDirectories();
            if (directories2.Count != infoArray.Length)
            {
                foreach (DirectoryInfo info2 in infoArray)
                {
                    if (!info2.Name.Equals(ThumbnailUtilities.ThumbnailDirectory))
                    {
                        virtualPath = GetVirtualPath(token, info2.FullName);
                        if (!directories.Contains(virtualPath))
                        {
                            string text3 = virtualPath.Substring(0, (virtualPath.Length - info2.Name.Length) - 1);
                            PhotoDirectory photoDirectory = new PhotoDirectory(token, info2.Name, text3);
                            ydb.Insert(parent, photoDirectory);
                            directories.Add(photoDirectory);
                        }
                        directories2.Remove(virtualPath);
                    }
                }
                foreach (PhotoDirectory directory in directories2)
                {
                    ydb.Delete(directory);
                    new PhotoDB(token.DBConnection).DeleteDirectoryPhotos(directory);
                    directories.Remove(directory);
                }
            }
            return directories;
        }

        public static string GetRootDirectorie(SessionToken token, string projcode)
        {
            PhotoDirectoryDB ydb = new PhotoDirectoryDB(token.DBConnection);
            return ydb.GetRootDirectorie(token, projcode);
        }

        private static string GetVirtualPath(SessionToken token, string fullDirectoryPath)
        {
            return fullDirectoryPath.Substring(token.PhotosRootFullPath.Length, fullDirectoryPath.Length - token.PhotosRootFullPath.Length);
        }

        public static bool RootDirectorieExist(SessionToken token, string name)
        {
            PhotoDirectoryDB ydb = new PhotoDirectoryDB(token.DBConnection);
            return ydb.RootDirectorieExist(token, name);
        }
    }
}

