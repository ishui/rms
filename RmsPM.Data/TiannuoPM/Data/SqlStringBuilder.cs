namespace TiannuoPM.Data
{
    using System;
    using System.Text;

    [CLSCompliant(true)]
    public class SqlStringBuilder
    {
        private bool ignoreCase;
        private string junction;
        private StringBuilder sql;

        public SqlStringBuilder()
        {
            this.sql = new StringBuilder();
            this.junction = SqlUtil.AND;
            this.ignoreCase = false;
        }

        public SqlStringBuilder(bool ignoreCase)
        {
            this.sql = new StringBuilder();
            this.junction = SqlUtil.AND;
            this.ignoreCase = ignoreCase;
        }

        public SqlStringBuilder(bool ignoreCase, bool useAnd)
        {
            this.sql = new StringBuilder();
            this.junction = useAnd ? SqlUtil.AND : SqlUtil.OR;
            this.ignoreCase = ignoreCase;
        }

        public virtual SqlStringBuilder Append(string column, string searchText)
        {
            return this.Append(this.junction, column, searchText, this.ignoreCase);
        }

        public virtual SqlStringBuilder Append(string column, string searchText, bool ignoreCase)
        {
            return this.Append(this.junction, column, searchText, ignoreCase);
        }

        public virtual SqlStringBuilder Append(string junction, string column, string searchText, bool ignoreCase)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                this.AppendInternal(junction, this.Parse(column, searchText, ignoreCase));
            }
            return this;
        }

        public virtual SqlStringBuilder AppendEquals(string column, string value)
        {
            return this.AppendEquals(this.junction, column, value);
        }

        public virtual SqlStringBuilder AppendEquals(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.AppendInternal(junction, column, "=", SqlUtil.Encode(value, true));
            }
            return this;
        }

        public virtual SqlStringBuilder AppendGreaterThan(string column, string value)
        {
            return this.AppendGreaterThan(this.junction, column, value);
        }

        public virtual SqlStringBuilder AppendGreaterThan(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.AppendInternal(junction, column, ">", SqlUtil.Encode(value, true));
            }
            return this;
        }

        public virtual SqlStringBuilder AppendGreaterThanOrEqual(string column, string value)
        {
            return this.AppendGreaterThanOrEqual(this.junction, column, value);
        }

        public virtual SqlStringBuilder AppendGreaterThanOrEqual(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.AppendInternal(junction, column, ">=", SqlUtil.Encode(value, true));
            }
            return this;
        }

        public virtual SqlStringBuilder AppendIn(string column, string values)
        {
            return this.AppendIn(this.junction, column, values, true);
        }

        public virtual SqlStringBuilder AppendIn(string column, string values, bool encode)
        {
            return this.AppendIn(this.junction, column, values, encode);
        }

        public virtual SqlStringBuilder AppendIn(string junction, string column, string values)
        {
            return this.AppendIn(junction, column, values, true);
        }

        public virtual SqlStringBuilder AppendIn(string junction, string column, string values, bool encode)
        {
            if (!string.IsNullOrEmpty(values))
            {
                values = this.GetInQueryValues(values, encode);
                if (!string.IsNullOrEmpty(values))
                {
                    this.AppendInQuery(junction, column, values);
                }
            }
            return this;
        }

        public virtual SqlStringBuilder AppendInQuery(string column, string query)
        {
            return this.AppendInQuery(this.junction, column, query);
        }

        public virtual SqlStringBuilder AppendInQuery(string junction, string column, string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                this.AppendInternal(junction, column, "IN", "(" + query + ")");
            }
            return this;
        }

        protected virtual void AppendInternal(string junction, string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                string newLine = Environment.NewLine;
                string format = (this.sql.Length > 0) ? " {0} ({1}){2}" : " ({1}){2}";
                this.sql.AppendFormat(format, junction, query, newLine);
            }
        }

        protected virtual void AppendInternal(string junction, string column, string compare, string value)
        {
            this.AppendInternal(junction, string.Format("{0} {1} {2}", column, compare, value));
        }

        public virtual SqlStringBuilder AppendIsNotNull(string column)
        {
            return this.AppendIsNotNull(this.junction, column);
        }

        public virtual SqlStringBuilder AppendIsNotNull(string junction, string column)
        {
            this.AppendInternal(junction, SqlUtil.IsNotNull(column));
            return this;
        }

        public virtual SqlStringBuilder AppendIsNull(string column)
        {
            return this.AppendIsNull(this.junction, column);
        }

        public virtual SqlStringBuilder AppendIsNull(string junction, string column)
        {
            this.AppendInternal(junction, SqlUtil.IsNull(column));
            return this;
        }

        public virtual SqlStringBuilder AppendLessThan(string column, string value)
        {
            return this.AppendLessThan(this.junction, column, value);
        }

        public virtual SqlStringBuilder AppendLessThan(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.AppendInternal(junction, column, "<", SqlUtil.Encode(value, true));
            }
            return this;
        }

        public virtual SqlStringBuilder AppendLessThanOrEqual(string column, string value)
        {
            return this.AppendLessThanOrEqual(this.junction, column, value);
        }

        public virtual SqlStringBuilder AppendLessThanOrEqual(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.AppendInternal(junction, column, "<=", SqlUtil.Encode(value, true));
            }
            return this;
        }

        public virtual SqlStringBuilder AppendNotEquals(string column, string value)
        {
            return this.AppendNotEquals(this.junction, column, value);
        }

        public virtual SqlStringBuilder AppendNotEquals(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.AppendInternal(junction, column, "<>", SqlUtil.Encode(value, true));
            }
            return this;
        }

        public virtual SqlStringBuilder AppendNotIn(string column, string values)
        {
            return this.AppendNotIn(this.junction, column, values, true);
        }

        public virtual SqlStringBuilder AppendNotIn(string column, string values, bool encode)
        {
            return this.AppendNotIn(this.junction, column, values, encode);
        }

        public virtual SqlStringBuilder AppendNotIn(string junction, string column, string values)
        {
            return this.AppendNotIn(junction, column, values, true);
        }

        public virtual SqlStringBuilder AppendNotIn(string junction, string column, string values, bool encode)
        {
            if (!string.IsNullOrEmpty(values))
            {
                values = this.GetInQueryValues(values, encode);
                if (!string.IsNullOrEmpty(values))
                {
                    this.AppendNotInQuery(junction, column, values);
                }
            }
            return this;
        }

        public virtual SqlStringBuilder AppendNotInQuery(string column, string query)
        {
            return this.AppendNotInQuery(this.junction, column, query);
        }

        public virtual SqlStringBuilder AppendNotInQuery(string junction, string column, string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                this.AppendInternal(junction, column, "NOT IN", "(" + query + ")");
            }
            return this;
        }

        public virtual SqlStringBuilder AppendRange(string column, string from, string to)
        {
            return this.AppendRange(this.junction, column, from, to);
        }

        public virtual SqlStringBuilder AppendRange(string junction, string column, string from, string to)
        {
            if (!string.IsNullOrEmpty(from) || !string.IsNullOrEmpty(to))
            {
                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(from))
                {
                    builder.AppendFormat("{0} >= {1}", column, SqlUtil.Encode(from, true));
                }
                if (!(string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to)))
                {
                    builder.AppendFormat(" {0} ", SqlUtil.AND);
                }
                if (!string.IsNullOrEmpty(to))
                {
                    builder.AppendFormat("{0} <= {1}", column, SqlUtil.Encode(to, true));
                }
                this.AppendInternal(junction, builder.ToString());
            }
            return this;
        }

        public virtual void Clear()
        {
            this.sql.Length = 0;
        }

        public virtual string GetInQueryValues(string values, bool encode)
        {
            if (encode)
            {
                values = SqlUtil.Encode(values.Split(new char[] { ',' }), encode);
            }
            return values;
        }

        public virtual string Parse(string column, string searchText, bool ignoreCase)
        {
            return new SqlExpressionParser(column, ignoreCase).Parse(searchText);
        }

        public override string ToString()
        {
            return this.sql.ToString().TrimEnd(new char[0]);
        }

        public virtual string ToString(string junction)
        {
            if (this.sql.Length > 0)
            {
                return new StringBuilder(" ").Append(junction).Append(" ").Append(this.sql.ToString().Trim()).ToString();
            }
            return string.Empty;
        }

        public virtual bool IgnoreCase
        {
            get
            {
                return this.ignoreCase;
            }
            set
            {
                this.ignoreCase = value;
            }
        }

        public virtual string Junction
        {
            get
            {
                return this.junction;
            }
            set
            {
                this.junction = value;
            }
        }
    }
}

