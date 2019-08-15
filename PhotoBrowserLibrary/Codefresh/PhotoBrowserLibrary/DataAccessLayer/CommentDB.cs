namespace Codefresh.PhotoBrowserLibrary.DataAccessLayer
{
    using Codefresh.PhotoBrowserLibrary;
    using Codefresh.PhotoBrowserLibrary.Collections;
    using System;
    using System.Data;

    internal class CommentDB : DBBase
    {
        public CommentDB(IDbConnection conn) : base(conn)
        {
        }

        internal override void Delete(PhotoObjectBase obj)
        {
            throw new NotImplementedException("Cannot delete comment objects");
        }

        public Comments GetComments(SessionToken token, Photo photo)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "SELECT ID, Name, Comment, DateAdded FROM tblPhotoComments WHERE PhotoFullVirtualPath = ? ORDER BY DateAdded, ID";
            command.Parameters.Add(base.CreateStringParam("PhotoFullVirtualPath", photo.FullVirtualPath));
            Comments comments = new Comments();
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    Comment comment = new Comment(token, reader.GetInt32(0), reader.IsDBNull(1) ? "" : reader.GetString(1), reader.IsDBNull(2) ? "" : reader.GetString(2), reader.GetDateTime(3));
                    comments.Add(comment);
                }
            }
            return comments;
        }

        public int Insert(Photo photo, Comment comment)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "INSERT INTO tblPhotoComments (PhotoFullVirtualPath, Name, Comment, DateAdded) VALUES (?, ?, ?, ?)";
            command.Parameters.Add(base.CreateStringParam("PhotoFullVirtualPath", photo.FullVirtualPath));
            command.Parameters.Add(base.CreateStringParam("Name", comment.Name));
            command.Parameters.Add(base.CreateStringParam("Comment", comment.CommentText));
            command.Parameters.Add(base.CreateDateParam("DateAdded", comment.DateAdded));
            command.ExecuteNonQuery();
            return base.GetIdentityValue(comment);
        }
    }
}

