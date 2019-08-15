namespace TiannuoPM.Data.SqlClient
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections;
    using System.Data.Common;
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;
    using TiannuoPM.Data;

    [Serializable, XmlRoot("root")]
    public sealed class StoredProcedureProvider
    {
        private static StoredProcedureProvider current = null;
        private ArrayList procedures = new ArrayList();

        private static object Deserialize()
        {
            string text = "TiannuoPM.Data.SqlClient";
            string name = string.Format("{0}.Procedures.xml", text);
            Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            XmlSerializer serializer = new XmlSerializer(typeof(StoredProcedureProvider));
            return serializer.Deserialize(manifestResourceStream);
        }

        public static DbCommand GetCommandWrapper(Database database, string commandName, bool useStoredProcedure)
        {
            DbCommand storedProcCommand;
            if (useStoredProcedure)
            {
                storedProcCommand = database.GetStoredProcCommand(commandName);
                storedProcCommand.CommandTimeout = DataRepository.Provider.DefaultCommandTimeout;
                return storedProcCommand;
            }
            storedProcCommand = database.GetSqlStringCommand(GetProcedureBodyFromEmbeddedResource(commandName));
            storedProcCommand.CommandTimeout = DataRepository.Provider.DefaultCommandTimeout;
            return storedProcCommand;
        }

        public static DbCommand GetCommandWrapper(Database database, string commandName, Type columnEnum, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength)
        {
            string format = GetProcedureBodyFromEmbeddedResource(commandName);
            string text2 = Utility.ParseSortExpression(columnEnum, orderBy);
            string text3 = string.Empty;
            if (!((parameters == null) || string.IsNullOrEmpty(parameters.FilterExpression)))
            {
                text3 = string.Format("where {0}", parameters.FilterExpression);
            }
            format = string.Format(format, new object[] { text3, text2, start, start + pageLength });
            DbCommand sqlStringCommand = database.GetSqlStringCommand(format);
            sqlStringCommand.CommandTimeout = DataRepository.Provider.DefaultCommandTimeout;
            return sqlStringCommand;
        }

        private static string GetProcedureBodyFromEmbeddedResource(string name)
        {
            Procedure procedure = Current[name];
            if (procedure == null)
            {
                throw new ApplicationException("cannot find the query '" + name + "' in the embedded xml file.");
            }
            return Current[name].Body;
        }

        public void Serialize(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            FileStream stream = new FileStream(filename, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
            new XmlSerializer(base.GetType()).Serialize((Stream) stream, this);
            stream.Close();
        }

        public static StoredProcedureProvider Current
        {
            get
            {
                if (current == null)
                {
                    lock (typeof(StoredProcedureProvider))
                    {
                        if (current == null)
                        {
                            current = (StoredProcedureProvider) Deserialize();
                        }
                    }
                }
                return current;
            }
        }

        public Procedure this[int index]
        {
            get
            {
                return (Procedure) this.procedures[index];
            }
        }

        public Procedure this[string name]
        {
            get
            {
                foreach (Procedure procedure in this.procedures)
                {
                    if ((procedure.Name == name) || ((procedure.Owner + "." + procedure.Name) == name))
                    {
                        return procedure;
                    }
                }
                return null;
            }
        }

        [XmlArrayItem("procedure", typeof(Procedure)), XmlArray("procedures")]
        public ArrayList Procedures
        {
            get
            {
                return this.procedures;
            }
            set
            {
                this.procedures = value;
            }
        }
    }
}

