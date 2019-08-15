namespace Codefresh.PhotoBrowserLibrary.DataAccessLayer
{
    using Codefresh.PhotoBrowserLibrary;
    using Codefresh.PhotoBrowserLibrary.Collections;
    using System;
    using System.Data;

    internal class PhotoDB : DBBase
    {
        public PhotoDB(IDbConnection conn) : base(conn)
        {
        }

        internal override void Delete(PhotoObjectBase obj)
        {
            Photo photo = (Photo) obj;
            IDbCommand command = base.GetCommand();
            command.CommandText = "DELETE FROM tblPhotos WHERE ID = ?";
            command.Parameters.Add(base.CreateIntParam("ID", photo.Id));
            if (command.ExecuteNonQuery() != 1)
            {
                throw new Exception("Attempted to delete a Photo record that does not exist");
            }
        }

        public void DeleteDirectoryPhotos(PhotoDirectory dir)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "DELETE FROM tblPhotos WHERE DirectoryID = ?";
            command.Parameters.Add(base.CreateIntParam("DirectoryID", dir.Id));
            command.ExecuteNonQuery();
        }

        public Photos GetPhotos(SessionToken token, PhotoDirectory dir)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "SELECT ID, Name, VirtualPath, DateTaken, FileSize, ViewedCount FROM tblPhotos WHERE DirectoryID = ? ORDER BY DateTaken, Name";
            command.Parameters.Add(base.CreateIntParam("DirectoryID", dir.Id));
            Photos photos = new Photos();
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    Photo photo = new Photo(token, reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3), (long) reader.GetInt32(4), reader.GetInt32(5));
                    photos.Add(photo);
                }
            }
            return photos;
        }

        public void IncrementViewedCount(Photo photo)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "UPDATE tblPhotos SET ViewedCount = ViewedCount + 1 WHERE ID = ?";
            command.Parameters.Add(base.CreateIntParam("ID", photo.Id));
            if (command.ExecuteNonQuery() != 1)
            {
                throw new Exception("Attempted to increment the viewed count for a non-existant Photo record");
            }
        }

        internal int Insert(PhotoDirectory dir, Photo photo)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "INSERT INTO tblPhotos (DirectoryID, Name, VirtualPath, DateTaken, FileSize, ViewedCount) VALUES (?, ?, ?, ?, ?, 0)";
            command.Parameters.Add(base.CreateIntParam("DirectoryID", dir.Id));
            command.Parameters.Add(base.CreateStringParam("Name", photo.Name));
            command.Parameters.Add(base.CreateStringParam("VirtualPath", photo.VirtualPath));
            command.Parameters.Add(base.CreateDateTimeParam("DateTaken", photo.DateTaken));
            command.Parameters.Add(base.CreateLongParam("FileSize", photo.FileSize));
            command.ExecuteNonQuery();
            return base.GetIdentityValue(photo);
        }
    }
}

