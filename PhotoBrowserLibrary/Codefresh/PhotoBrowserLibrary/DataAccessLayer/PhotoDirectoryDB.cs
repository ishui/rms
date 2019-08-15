namespace Codefresh.PhotoBrowserLibrary.DataAccessLayer
{
    using Codefresh.PhotoBrowserLibrary;
    using Codefresh.PhotoBrowserLibrary.Collections;
    using System;
    using System.Data;

    internal class PhotoDirectoryDB : DBBase
    {
        public PhotoDirectoryDB(IDbConnection conn) : base(conn)
        {
        }

        public bool CreateRootDirectorie(SessionToken token, string projcode, string name)
        {
            try
            {
                IDbCommand command = base.GetCommand();
                command.CommandText = "INSERT INTO tblProjmap (ProjCode, Path) VALUES (?, ?)";
                command.Parameters.Add(base.CreateStringParam("ProjCode", projcode));
                command.Parameters.Add(base.CreateStringParam("Path", name));
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal override void Delete(PhotoObjectBase obj)
        {
            PhotoDirectory directory = (PhotoDirectory) obj;
            IDbCommand command = base.GetCommand();
            command.CommandText = "DELETE FROM tblDirectories WHERE ID = ?";
            command.Parameters.Add(base.CreateIntParam("ID", directory.Id));
            if (command.ExecuteNonQuery() != 1)
            {
                throw new Exception("Attempted to delete a PhotoDirectory record that did not exit");
            }
        }

        internal void Delete(string name, string virtualPath)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "select name,virtualpath FROM tblDirectories WHERE  VirtualPath = ?";
            command.Parameters.Add(base.CreateStringParam("VirtualPath", virtualPath + @"\" + name));
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    this.Delete(reader.GetString(0), reader.GetString(1));
                }
            }
            command.CommandText = "Delete from tblphotos  where virtualpath= ? ";
            command.Parameters.Add(base.CreateStringParam("virtualpath", virtualPath + @"\" + name));
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            command.CommandText = "DELETE FROM tblDirectories WHERE Name = ? AND VirtualPath = ?";
            command.Parameters.Add(base.CreateStringParam("Name", name));
            command.Parameters.Add(base.CreateStringParam("VirtualPath", virtualPath));
            command.ExecuteNonQuery();
        }

        public PhotoDirectories GetDirectorie(SessionToken token, string virtualPath, string name)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "SELECT ID, Name from tblDirectories where VirtualPath = ? and name= ? ";
            command.Parameters.Add(base.CreateStringParam("VirtualPath", virtualPath));
            command.Parameters.Add(base.CreateStringParam("name", name));
            PhotoDirectories directories = new PhotoDirectories();
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    PhotoDirectory photoDirectory = new PhotoDirectory(token, reader.GetInt32(0), reader.GetString(1), virtualPath);
                    directories.Add(photoDirectory);
                }
            }
            return directories;
        }

        public PhotoDirectories GetDirectories(SessionToken token, string virtualPath)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "SELECT ID, Name from tblDirectories where VirtualPath = ? ORDER BY Name";
            command.Parameters.Add(base.CreateStringParam("VirtualPath", virtualPath));
            PhotoDirectories directories = new PhotoDirectories();
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    PhotoDirectory photoDirectory = new PhotoDirectory(token, reader.GetInt32(0), reader.GetString(1), virtualPath);
                    directories.Add(photoDirectory);
                }
            }
            return directories;
        }

        public string GetRootDirectorie(SessionToken token, string projcode)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "SELECT ProjCode,Path from tblProjMap where ProjCode= ? ";
            command.Parameters.Add(base.CreateStringParam("ProjCode", projcode));
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    return reader.GetString(1);
                }
                return null;
            }
        }

        internal int Insert(PhotoDirectory parent, PhotoDirectory photoDirectory)
        {
            IDbCommand command = base.GetCommand();
            if (parent == null)
            {
                command.CommandText = "INSERT INTO tblDirectories (ParentID, Name, VirtualPath) VALUES (NULL, ?, ?)";
            }
            else
            {
                command.CommandText = "INSERT INTO tblDirectories (ParentID, Name, VirtualPath) VALUES (?, ?, ?)";
                command.Parameters.Add(base.CreateIntParam("ParentID", parent.Id));
            }
            command.Parameters.Add(base.CreateStringParam("Name", photoDirectory.Name));
            command.Parameters.Add(base.CreateStringParam("VirtualPath", photoDirectory.VirtualPath));
            command.ExecuteNonQuery();
            return base.GetIdentityValue(photoDirectory);
        }

        public bool RootDirectorieExist(SessionToken token, string name)
        {
            IDbCommand command = base.GetCommand();
            command.CommandText = "SELECT ProjCode,Path from tblProjMap where Path= ? ";
            command.Parameters.Add(base.CreateStringParam("Path", name));
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.Default))
            {
                return reader.Read();
            }
        }
    }
}

