namespace Codefresh.PhotoBrowserLibrary
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public sealed class ThumbnailUtilities
    {
        private const string THUMBNAIL_DIR = "Thumbnails";

        private ThumbnailUtilities()
        {
        }

        public static string CreateThumbnail(string imagePath)
        {
            using (Bitmap bitmap = new Bitmap(imagePath))
            {
                int num = 150;
                int num2 = 150;
                int width = bitmap.Width;
                int height = bitmap.Height;
                float num5 = width / num;
                float num6 = height / num2;
                float num7 = Math.Max(num5, num6);
                if (num7 < 1f)
                {
                    num7 = 1f;
                }
                int thumbWidth = (int) (((float) width) / num7);
                int thumbHeight = (int) (((float) height) / num7);
                Bitmap bitmap2 = (Bitmap) bitmap.GetThumbnailImage(thumbWidth, thumbHeight, new Image.GetThumbnailImageAbort(ThumbnailUtilities.ThumbnailCallback), IntPtr.Zero);
                string path = GetDirectory(imagePath) + Path.DirectorySeparatorChar + ThumbnailDirectory;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filename = path + Path.DirectorySeparatorChar + GetFilename(imagePath);
                bitmap2.Save(filename, ImageFormat.Jpeg);
                return GetFilename(imagePath);
            }
        }

        public static void DeleteThumbnail(string imagePath)
        {
            File.Delete(imagePath);
        }

        private static string GetDirectory(string path)
        {
            FileInfo info = new FileInfo(path);
            return info.Directory.FullName;
        }

        private static string GetFilename(string path)
        {
            FileInfo info = new FileInfo(path);
            return info.Name;
        }

        private static bool ThumbnailCallback()
        {
            return false;
        }

        public static string ThumbnailDirectory
        {
            get
            {
                return "Thumbnails";
            }
        }
    }
}

