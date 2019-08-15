namespace Codefresh.PhotoBrowserLibrary
{
    using Codefresh.PhotoBrowserLibrary.Collections;
    using Codefresh.PhotoBrowserLibrary.DataAccessLayer;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;

    public sealed class PhotoUtilities
    {
        private PhotoUtilities()
        {
        }

        public static void DeletePhoto(SessionToken token, Photo photo, string rootpath)
        {
            new PhotoDB(token.DBConnection).Delete(photo);
            photo.BeDeleted = true;
            File.Delete(rootpath + photo.FullVirtualPath);
            File.Delete(rootpath + photo.FullThumbnailVirtualPath);
        }

        public static Photos GetPhotos(SessionToken token, PhotoDirectory obj)
        {
            PhotoDB odb = new PhotoDB(token.DBConnection);
            Photos photos = odb.GetPhotos(token, obj);
            Photos photos2 = (Photos) photos.Clone();
            FileInfo[] files = new DirectoryInfo(token.MapPath(obj.FullVirtualPath)).GetFiles("*.*");
            if (photos.Count != files.Length)
            {
                foreach (FileInfo info2 in files)
                {
                    string fullVirtualPath = GetVirtualPath(token, info2.FullName);
                    if (!photos.Contains(fullVirtualPath))
                    {
                        string virtualPath = fullVirtualPath.Substring(0, (fullVirtualPath.Length - info2.Name.Length) - 1);
                        DateTime dateTaken = DateTime.MinValue;
                        try
                        {
                            using (Image image = Image.FromFile(info2.FullName))
                            {
                                try
                                {
                                    dateTaken = ParseExifDateTime(image.GetPropertyItem(0x9003).Value);
                                }
                                catch (ArgumentException)
                                {
                                }
                            }
                            Photo photo = new Photo(token, info2.Name, virtualPath, dateTaken, info2.Length);
                            odb.Insert(obj, photo);
                            photos.Add(photo);
                        }
                        catch
                        {
                        }
                    }
                    photos2.Remove(fullVirtualPath);
                }
                foreach (Photo photo in photos2)
                {
                    ThumbnailUtilities.DeleteThumbnail(token.MapPath(photo.FullThumbnailVirtualPath));
                    odb.Delete(photo);
                    photos.Remove(photo);
                }
            }
            return photos;
        }

        private static string GetVirtualPath(SessionToken token, string fullDirectoryPath)
        {
            return fullDirectoryPath.Substring(token.PhotosRootFullPath.Length, fullDirectoryPath.Length - token.PhotosRootFullPath.Length);
        }

        public static DateTime ParseExifDateTime(byte[] data)
        {
            DateTime minValue = DateTime.MinValue;
            string text = ParseExifString(data);
            if (text.Length < 0x13)
            {
                return minValue;
            }
            try
            {
                int year = int.Parse(text.Substring(0, 4), CultureInfo.CurrentCulture);
                int month = int.Parse(text.Substring(5, 2), CultureInfo.CurrentCulture);
                int day = int.Parse(text.Substring(8, 2), CultureInfo.CurrentCulture);
                int hour = int.Parse(text.Substring(11, 2), CultureInfo.CurrentCulture);
                int minute = int.Parse(text.Substring(14, 2), CultureInfo.CurrentCulture);
                return new DateTime(year, month, day, hour, minute, int.Parse(text.Substring(0x11, 2), CultureInfo.CurrentCulture));
            }
            catch (FormatException)
            {
                return minValue;
            }
        }

        public static string ParseExifString(byte[] data)
        {
            string text = "";
            if (data.Length > 1)
            {
                IntPtr ptr = Marshal.AllocHGlobal(data.Length);
                int ofs = 0;
                foreach (byte num2 in data)
                {
                    Marshal.WriteByte(ptr, ofs, num2);
                    ofs++;
                }
                text = Marshal.PtrToStringAnsi(ptr);
                Marshal.FreeHGlobal(ptr);
            }
            return text;
        }
    }
}

