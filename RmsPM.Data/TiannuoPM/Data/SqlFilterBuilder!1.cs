namespace TiannuoPM.Data
{
    using System;
    using TiannuoPM.Entities;

    [CLSCompliant(true)]
    public class SqlFilterBuilder<EntityColumn> : SqlStringBuilder
    {
        public SqlFilterBuilder()
        {
        }

        public SqlFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public SqlFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }

        public virtual SqlFilterBuilder<EntityColumn> Append(EntityColumn column, string searchText)
        {
            return this.Append(this.Junction, column, searchText, this.IgnoreCase);
        }

        public virtual SqlFilterBuilder<EntityColumn> Append(EntityColumn column, string searchText, bool ignoreCase)
        {
            return this.Append(this.Junction, column, searchText, ignoreCase);
        }

        public virtual SqlFilterBuilder<EntityColumn> Append(string junction, EntityColumn column, string searchText, bool ignoreCase)
        {
            this.Append(junction, this.GetColumnName(column), searchText, ignoreCase);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendEquals(EntityColumn column, string value)
        {
            return this.AppendEquals(this.Junction, column, value);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendEquals(string junction, EntityColumn column, string value)
        {
            this.AppendEquals(junction, this.GetColumnName(column), value);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendGreaterThan(EntityColumn column, string value)
        {
            return this.AppendGreaterThan(this.Junction, column, value);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendGreaterThan(string junction, EntityColumn column, string value)
        {
            this.AppendGreaterThan(junction, this.GetColumnName(column), value);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendGreaterThanOrEqual(EntityColumn column, string value)
        {
            return this.AppendGreaterThanOrEqual(this.Junction, column, value);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendGreaterThanOrEqual(string junction, EntityColumn column, string value)
        {
            this.AppendGreaterThanOrEqual(junction, this.GetColumnName(column), value);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendIn(EntityColumn column, string values)
        {
            return this.AppendIn(this.Junction, column, values, true);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendIn(string junction, EntityColumn column, string values)
        {
            return this.AppendIn(junction, column, values, true);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendIn(EntityColumn column, string values, bool encode)
        {
            return this.AppendIn(this.Junction, column, values, encode);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendIn(string junction, EntityColumn column, string values, bool encode)
        {
            this.AppendIn(junction, this.GetColumnName(column), values, encode);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendInQuery(EntityColumn column, string query)
        {
            return this.AppendInQuery(this.Junction, column, query);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendInQuery(string junction, EntityColumn column, string query)
        {
            this.AppendInQuery(junction, this.GetColumnName(column), query);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendIsNotNull(EntityColumn column)
        {
            return this.AppendIsNotNull(this.Junction, column);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendIsNotNull(string junction, EntityColumn column)
        {
            this.AppendIsNotNull(junction, this.GetColumnName(column));
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendIsNull(EntityColumn column)
        {
            return this.AppendIsNull(this.Junction, column);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendIsNull(string junction, EntityColumn column)
        {
            this.AppendIsNull(junction, this.GetColumnName(column));
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendLessThan(EntityColumn column, string value)
        {
            return this.AppendLessThan(this.Junction, column, value);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendLessThan(string junction, EntityColumn column, string value)
        {
            this.AppendLessThan(junction, this.GetColumnName(column), value);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendLessThanOrEqual(EntityColumn column, string value)
        {
            return this.AppendLessThanOrEqual(this.Junction, column, value);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendLessThanOrEqual(string junction, EntityColumn column, string value)
        {
            this.AppendLessThanOrEqual(junction, this.GetColumnName(column), value);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendNotEquals(EntityColumn column, string value)
        {
            return this.AppendNotEquals(this.Junction, column, value);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendNotEquals(string junction, EntityColumn column, string value)
        {
            this.AppendNotEquals(junction, this.GetColumnName(column), value);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendNotIn(EntityColumn column, string values)
        {
            return this.AppendNotIn(this.Junction, column, values, true);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendNotIn(EntityColumn column, string values, bool encode)
        {
            return this.AppendNotIn(this.Junction, column, values, encode);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendNotIn(string junction, EntityColumn column, string values)
        {
            return this.AppendNotIn(junction, column, values, true);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendNotIn(string junction, EntityColumn column, string values, bool encode)
        {
            this.AppendNotIn(junction, this.GetColumnName(column), values, encode);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendNotInQuery(EntityColumn column, string query)
        {
            return this.AppendNotInQuery(this.Junction, column, query);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendNotInQuery(string junction, EntityColumn column, string query)
        {
            this.AppendNotInQuery(junction, this.GetColumnName(column), query);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendRange(EntityColumn column, string from, string to)
        {
            return this.AppendRange(this.Junction, column, from, to);
        }

        public virtual SqlFilterBuilder<EntityColumn> AppendRange(string junction, EntityColumn column, string from, string to)
        {
            this.AppendRange(junction, this.GetColumnName(column), from, to);
            return (SqlFilterBuilder<EntityColumn>) this;
        }

        protected virtual string GetColumnName(EntityColumn column)
        {
            string enumTextValue = EntityHelper.GetEnumTextValue(column as Enum);
            if (string.IsNullOrEmpty(enumTextValue))
            {
                enumTextValue = column.ToString();
            }
            return enumTextValue;
        }
    }
}

