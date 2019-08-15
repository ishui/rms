namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.Collections;
    using System.Xml.Serialization;

    [Serializable]
    public class Procedure
    {
        private string body;
        private string comment;
        private string name;
        private string owner;
        private ArrayList parameters = new ArrayList();

        public TiannuoPM.Data.SqlClient.Parameter Parameter(string paramName)
        {
            foreach (TiannuoPM.Data.SqlClient.Parameter parameter in this.parameters)
            {
                if (parameter.Name == paramName)
                {
                    return parameter;
                }
            }
            return null;
        }

        [XmlElement("body")]
        public string Body
        {
            get
            {
                return this.body;
            }
            set
            {
                this.body = value;
            }
        }

        [XmlElement("comment")]
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
            }
        }

        [XmlAttribute("name")]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        [XmlAttribute("owner")]
        public string Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }

        [XmlArray("parameters"), XmlArrayItem("parameter", typeof(TiannuoPM.Data.SqlClient.Parameter))]
        public ArrayList Parameters
        {
            get
            {
                return this.parameters;
            }
            set
            {
                this.parameters = value;
            }
        }
    }
}

