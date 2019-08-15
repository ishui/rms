namespace TiannuoPM.Data
{
    using System;
    using System.Configuration;
    using System.Text;

    [CLSCompliant(true)]
    public class ParameterizedSqlFilterBuilder<EntityColumn> : SqlFilterBuilder<EntityColumn>
    {
        private SqlFilterParameterCollection parameters;

        public ParameterizedSqlFilterBuilder()
        {
        }

        public ParameterizedSqlFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ParameterizedSqlFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }

        public override SqlFilterBuilder<EntityColumn> Append(string junction, EntityColumn column, string searchText, bool ignoreCase)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.Append(junction, column, searchText, ignoreCase);
        }

        public override SqlFilterBuilder<EntityColumn> AppendEquals(string junction, EntityColumn column, string value)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendEquals(junction, column, value);
        }

        public override SqlStringBuilder AppendEquals(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = SqlUtil.Equals(value);
                this.AppendInternal(junction, column, "=", this.Parameters.GetParameter(value));
            }
            return this;
        }

        public override SqlFilterBuilder<EntityColumn> AppendGreaterThan(string junction, EntityColumn column, string value)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendGreaterThan(junction, column, value);
        }

        public override SqlStringBuilder AppendGreaterThan(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = SqlUtil.Equals(value);
                this.AppendInternal(junction, column, ">", this.Parameters.GetParameter(value));
            }
            return this;
        }

        public override SqlStringBuilder AppendGreaterThanOrEqual(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = SqlUtil.Equals(value);
                this.AppendInternal(junction, column, ">=", this.Parameters.GetParameter(value));
            }
            return this;
        }

        public override SqlFilterBuilder<EntityColumn> AppendGreaterThanOrEqual(string junction, EntityColumn column, string value)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendGreaterThanOrEqual(junction, column, value);
        }

        public override SqlStringBuilder AppendIn(string junction, string column, string values, bool encode)
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

        public override SqlFilterBuilder<EntityColumn> AppendIn(string junction, EntityColumn column, string values, bool encode)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendIn(junction, column, values, encode);
        }

        public override SqlFilterBuilder<EntityColumn> AppendInQuery(string junction, EntityColumn column, string query)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendInQuery(junction, column, query);
        }

        public override SqlFilterBuilder<EntityColumn> AppendLessThan(string junction, EntityColumn column, string value)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendLessThan(junction, column, value);
        }

        public override SqlStringBuilder AppendLessThan(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = SqlUtil.Equals(value);
                this.AppendInternal(junction, column, "<", this.Parameters.GetParameter(value));
            }
            return this;
        }

        public override SqlFilterBuilder<EntityColumn> AppendLessThanOrEqual(string junction, EntityColumn column, string value)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendLessThanOrEqual(junction, column, value);
        }

        public override SqlStringBuilder AppendLessThanOrEqual(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = SqlUtil.Equals(value);
                this.AppendInternal(junction, column, "<=", this.Parameters.GetParameter(value));
            }
            return this;
        }

        public override SqlFilterBuilder<EntityColumn> AppendNotEquals(string junction, EntityColumn column, string value)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendNotEquals(junction, column, value);
        }

        public override SqlStringBuilder AppendNotEquals(string junction, string column, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = SqlUtil.Equals(value);
                this.AppendInternal(junction, column, "<>", this.Parameters.GetParameter(value));
            }
            return this;
        }

        public override SqlFilterBuilder<EntityColumn> AppendNotIn(string junction, EntityColumn column, string values, bool encode)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendNotIn(junction, column, values, encode);
        }

        public override SqlStringBuilder AppendNotIn(string junction, string column, string values, bool encode)
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

        public override SqlFilterBuilder<EntityColumn> AppendNotInQuery(string junction, EntityColumn column, string query)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendNotInQuery(junction, column, query);
        }

        public override SqlFilterBuilder<EntityColumn> AppendRange(string junction, EntityColumn column, string from, string to)
        {
            this.Parameters.SetCurrentColumn(column);
            return base.AppendRange(junction, column, from, to);
        }

        public override SqlStringBuilder AppendRange(string junction, string column, string from, string to)
        {
            if (!string.IsNullOrEmpty(from) || !string.IsNullOrEmpty(to))
            {
                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(from))
                {
                    builder.AppendFormat("{0} >= {1}", column, this.Parameters.GetParameter(from));
                }
                if (!(string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to)))
                {
                    builder.AppendFormat(" {0} ", SqlUtil.AND);
                }
                if (!string.IsNullOrEmpty(to))
                {
                    builder.AppendFormat("{0} <= {1}", column, this.Parameters.GetParameter(to));
                }
                this.AppendInternal(junction, builder.ToString());
            }
            return this;
        }

        public override string GetInQueryValues(string values, bool encode)
        {
            CommaDelimitedStringCollection strings = new CommaDelimitedStringCollection();
            string[] textArray = values.Split(new char[] { ',' });
            foreach (string text2 in textArray)
            {
                string text = text2.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    strings.Add(this.Parameters.GetParameter(text));
                }
            }
            return strings.ToString();
        }

        public virtual SqlFilterParameterCollection GetParameters()
        {
            this.Parameters.FilterExpression = this.ToString();
            return this.Parameters;
        }

        public override string Parse(string column, string searchText, bool ignoreCase)
        {
            ParameterizedSqlExpressionParser parser = new ParameterizedSqlExpressionParser(column, ignoreCase);
            parser.Parameters = this.Parameters;
            return parser.Parse(searchText);
        }

        public virtual SqlFilterParameterCollection Parameters
        {
            get
            {
                if (this.parameters == null)
                {
                    this.parameters = new SqlFilterParameterCollection();
                }
                return this.parameters;
            }
            set
            {
                this.parameters = value;
            }
        }
    }
}

