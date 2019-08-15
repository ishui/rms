namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    public sealed class ColumnEnumAttribute : Attribute
    {
        private bool allowDbNull;
        private System.Data.DbType dbType;
        private bool isIdentity;
        private bool isPrimaryKey;
        private int length;
        private string name;
        private Type systemType;

        public ColumnEnumAttribute(string name, Type systemType, System.Data.DbType dbType, bool isPrimaryKey, bool isIdentity, bool allowDbNull) : this(name, systemType, dbType, isPrimaryKey, isIdentity, allowDbNull, -1)
        {
        }

        public ColumnEnumAttribute(string name, Type systemType, System.Data.DbType dbType, bool isPrimaryKey, bool isIdentity, bool allowDbNull, int length)
        {
            this.Name = name;
            this.SystemType = systemType;
            this.DbType = dbType;
            this.IsPrimaryKey = isPrimaryKey;
            this.IsIdentity = isIdentity;
            this.AllowDbNull = allowDbNull;
            this.Length = length;
        }

        public bool AllowDbNull
        {
            get
            {
                return this.allowDbNull;
            }
            set
            {
                this.allowDbNull = value;
            }
        }

        public System.Data.DbType DbType
        {
            get
            {
                return this.dbType;
            }
            set
            {
                this.dbType = value;
            }
        }

        public bool IsIdentity
        {
            get
            {
                return this.isIdentity;
            }
            set
            {
                this.isIdentity = value;
            }
        }

        public bool IsPrimaryKey
        {
            get
            {
                return this.isPrimaryKey;
            }
            set
            {
                this.isPrimaryKey = value;
            }
        }

        public int Length
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
            }
        }

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

        public Type SystemType
        {
            get
            {
                return this.systemType;
            }
            set
            {
                this.systemType = value;
            }
        }
    }
}

