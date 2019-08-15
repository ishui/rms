namespace TiannuoPM.Data
{
    using System;
    using System.Data;
    using TiannuoPM.Entities;

    [Serializable, CLSCompliant(true)]
    public class SqlFilterParameter
    {
        private Enum column;
        private int parameterIndex;
        private string parameterValue;

        public SqlFilterParameter(Enum column, string value, int index)
        {
            this.column = column;
            this.parameterValue = value;
            this.parameterIndex = index;
        }

        public Enum Column
        {
            get
            {
                return this.column;
            }
            set
            {
                this.column = value;
            }
        }

        public System.Data.DbType DbType
        {
            get
            {
                System.Data.DbType dbType = System.Data.DbType.String;

               // if (this.column != 0)
                {
                    ColumnEnumAttribute attribute = EntityHelper.GetAttribute<ColumnEnumAttribute>(this.column);
                    if (attribute != null)
                    {
                        dbType = attribute.DbType;
                    }
                }
                return dbType;
            }
        }

        public int Index
        {
            get
            {
                return this.parameterIndex;
            }
        }

        public string Name
        {
            get
            {
                return string.Format("@Param{0}", this.Index);
            }
        }

        public string Value
        {
            get
            {
                return this.parameterValue;
            }
            set
            {
                this.parameterValue = value;
            }
        }
    }
}

