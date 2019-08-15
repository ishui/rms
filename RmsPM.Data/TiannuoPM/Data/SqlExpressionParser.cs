namespace TiannuoPM.Data
{
    using System;
    using System.Text;

    [CLSCompliant(true)]
    public class SqlExpressionParser : ExpressionParserBase
    {
        private StringBuilder sql;

        public SqlExpressionParser(string propertyName) : this(propertyName, SqlComparisonType.Contains, false)
        {
        }

        public SqlExpressionParser(string propertyName, bool ignoreCase) : this(propertyName, SqlComparisonType.Contains, ignoreCase)
        {
        }

        public SqlExpressionParser(string propertyName, SqlComparisonType comparisonType) : this(propertyName, comparisonType, false)
        {
        }

        public SqlExpressionParser(string propertyName, SqlComparisonType comparisonType, bool ignoreCase) : base(propertyName, comparisonType, ignoreCase)
        {
        }

        protected override void AppendAnd()
        {
            this.sql.AppendFormat(" {0} ", SqlUtil.AND);
        }

        protected override void AppendOr()
        {
            this.sql.AppendFormat(" {0} ", SqlUtil.OR);
        }

        protected override void AppendSearchText(string searchText)
        {
            this.sql.Append(this.WrapWithSQL(base.PropertyName, searchText, base.IgnoreCase));
        }

        protected override void AppendSpace()
        {
            this.sql.Append(" ");
        }

        protected override void CloseGrouping()
        {
            this.sql.Append(SqlUtil.RIGHT);
        }

        protected virtual string Contains(string column, string value, bool ignoreCase)
        {
            return SqlUtil.Contains(column, value, ignoreCase);
        }

        protected virtual string EndsWith(string column, string value, bool ignoreCase)
        {
            return SqlUtil.EndsWith(column, value, ignoreCase);
        }

        protected virtual string Equals(string column, string value, bool ignoreCase)
        {
            return SqlUtil.Equals(column, value, ignoreCase);
        }

        protected virtual string Like(string column, string value, bool ignoreCase)
        {
            return SqlUtil.Like(column, value, ignoreCase);
        }

        protected override void OpenGrouping()
        {
            this.sql.Append(SqlUtil.LEFT);
        }

        public virtual string Parse(string value)
        {
            this.sql = new StringBuilder();
            base.ParseCore(value);
            return this.sql.ToString();
        }

        protected virtual string StartsWith(string column, string value, bool ignoreCase)
        {
            return SqlUtil.StartsWith(column, value, ignoreCase);
        }

        protected virtual string WrapWithSQL(string propertyName, string value, bool ignoreCase)
        {
            SqlComparisonType comparisonType = base.ComparisonType;
            string text = string.Empty;
            if (string.IsNullOrEmpty(value))
            {
                return text;
            }
            if (value.Equals(SqlUtil.STAR))
            {
                comparisonType = SqlComparisonType.Like;
                value = SqlUtil.WILD;
            }
            else if (value.StartsWith(SqlUtil.STAR) && value.EndsWith(SqlUtil.STAR))
            {
                comparisonType = SqlComparisonType.Contains;
                value = value.Substring(1, value.Length - 2);
            }
            else if (value.EndsWith(SqlUtil.STAR))
            {
                comparisonType = SqlComparisonType.StartsWith;
                value = value.Substring(0, value.Length - 1);
            }
            else if (value.StartsWith(SqlUtil.STAR))
            {
                comparisonType = SqlComparisonType.EndsWith;
                value = value.Substring(1, value.Length - 1);
            }
            else
            {
                comparisonType = SqlComparisonType.Equals;
            }
            if (value.IndexOf(SqlUtil.STAR) > -1)
            {
                value = value.Replace(SqlUtil.STAR, SqlUtil.WILD);
                if (comparisonType == SqlComparisonType.Equals)
                {
                    comparisonType = SqlComparisonType.Like;
                }
            }
            if ((comparisonType == SqlComparisonType.Equals) && (value.IndexOf(SqlUtil.WILD) > -1))
            {
                comparisonType = SqlComparisonType.Like;
            }
            switch (comparisonType)
            {
                case SqlComparisonType.StartsWith:
                    return this.StartsWith(propertyName, value, ignoreCase);

                case SqlComparisonType.EndsWith:
                    return this.EndsWith(propertyName, value, ignoreCase);

                case SqlComparisonType.Contains:
                    return this.Contains(propertyName, value, ignoreCase);

                case SqlComparisonType.Like:
                    return this.Like(propertyName, value, ignoreCase);
            }
            return this.Equals(propertyName, value, ignoreCase);
        }
    }
}

