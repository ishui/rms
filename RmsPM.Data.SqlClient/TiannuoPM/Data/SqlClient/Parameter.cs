namespace TiannuoPM.Data.SqlClient
{
    using System;
    using System.Data;
    using System.Xml.Serialization;

    [Serializable]
    public class Parameter
    {
        private ParameterDirection direction;
        private string name;
        private int precision;
        private int scale;
        private int size;
        private string sqltype;

        [XmlAttribute("direction")]
        public ParameterDirection Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
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

        [XmlAttribute("precision")]
        public int Precision
        {
            get
            {
                return this.precision;
            }
            set
            {
                this.precision = value;
            }
        }

        [XmlAttribute("scale")]
        public int Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;
            }
        }

        [XmlAttribute("size")]
        public int Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        [XmlAttribute("type")]
        public string SqlType
        {
            get
            {
                return this.sqltype;
            }
            set
            {
                this.sqltype = value;
            }
        }
    }
}

