namespace TiannuoPM.Data
{
    using System;

    [CLSCompliant(true)]
    public class ParameterizedSqlExpressionParser : SqlExpressionParser
    {
        private SqlFilterParameterCollection parameters;

        public ParameterizedSqlExpressionParser(string propertyName) : this(propertyName, SqlComparisonType.Contains, false)
        {
        }

        public ParameterizedSqlExpressionParser(string propertyName, bool ignoreCase) : this(propertyName, SqlComparisonType.Contains, ignoreCase)
        {
        }

        public ParameterizedSqlExpressionParser(string propertyName, SqlComparisonType comparisonType) : this(propertyName, comparisonType, false)
        {
        }

        public ParameterizedSqlExpressionParser(string propertyName, SqlComparisonType comparisonType, bool ignoreCase) : base(propertyName, comparisonType, ignoreCase)
        {
        }

        protected override string Contains(string column, string value, bool ignoreCase)
        {
            value = SqlUtil.Contains(value);
            return SqlUtil.Like(column, this.Parameters.GetParameter(value), ignoreCase, false);
        }

        protected override string EndsWith(string column, string value, bool ignoreCase)
        {
            value = SqlUtil.EndsWith(value);
            return SqlUtil.Like(column, this.Parameters.GetParameter(value), ignoreCase, false);
        }

        protected override string Equals(string column, string value, bool ignoreCase)
        {
            value = SqlUtil.Equals(value);
            return SqlUtil.Equals(column, this.Parameters.GetParameter(value), ignoreCase, false);
        }

        protected override string Like(string column, string value, bool ignoreCase)
        {
            value = SqlUtil.Like(value);
            return SqlUtil.Like(column, this.Parameters.GetParameter(value), ignoreCase, false);
        }

        protected override string StartsWith(string column, string value, bool ignoreCase)
        {
            value = SqlUtil.StartsWith(value);
            return SqlUtil.Like(column, this.Parameters.GetParameter(value), ignoreCase, false);
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

