namespace TiannuoPM.Data
{
    using System;
    using System.Collections.Generic;

    [Serializable, CLSCompliant(true)]
    public class SqlFilterParameterCollection : List<SqlFilterParameter>
    {
        private Enum currentColumn;
        private string filterExpression;

        public string GetParameter(string value)
        {
            SqlFilterParameter item = new SqlFilterParameter(this.CurrentColumn, value, base.Count);
            base.Add(item);
            return item.Name;
        }

        public void SetCurrentColumn(object column)
        {
            this.currentColumn = (Enum) column;
        }

        public Enum CurrentColumn
        {
            get
            {
                return this.currentColumn;
            }
        }

        public string FilterExpression
        {
            get
            {
                return this.filterExpression;
            }
            set
            {
                this.filterExpression = value;
            }
        }
    }
}

