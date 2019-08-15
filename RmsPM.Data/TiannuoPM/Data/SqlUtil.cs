namespace TiannuoPM.Data
{
    using System;
    using System.Configuration;

    [CLSCompliant(true)]
    public static class SqlUtil
    {
        public static readonly string AND = "AND";
        public static readonly string ASC = "ASC";
        public static readonly string COMMA = ",";
        public static readonly string DESC = "DESC";
        public static readonly string LEFT = "(";
        public static readonly string NULL = "NULL";
        public static readonly string OR = "OR";
        public static readonly string QUOTE = "\"";
        public static readonly string RIGHT = ")";
        public static readonly string STAR = "*";
        public static readonly string TOKEN = "@@@";
        public static readonly string WILD = "%";

        public static string Contains(string value)
        {
            return string.Format("%{0}%", Encode(value));
        }

        public static string Contains(string column, string value)
        {
            return Contains(column, value, false);
        }

        public static string Contains(string column, string value, bool ignoreCase)
        {
            return Contains(column, value, ignoreCase, true);
        }

        public static string Contains(string column, string value, bool ignoreCase, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return IsNull(column);
            }
            return string.Format(GetLikeFormat(ignoreCase, surround), column, Contains(value));
        }

        public static string Encode(string value)
        {
            return Encode(value, false);
        }

        public static string Encode(string[] values)
        {
            return Encode(values, false);
        }

        public static string Encode(string value, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return NULL;
            }
            string format = surround ? "'{0}'" : "{0}";
            return string.Format(format, value.Replace("'", "''"));
        }

        public static string Encode(string[] values, bool surround)
        {
            if ((values == null) || (values.Length < 1))
            {
                return NULL;
            }
            CommaDelimitedStringCollection strings = new CommaDelimitedStringCollection();
            foreach (string text in values)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    strings.Add(Encode(text.Trim(), surround));
                }
            }
            return strings.ToString();
        }

        public static string EndsWith(string value)
        {
            return string.Format("%{0}", Encode(value));
        }

        public static string EndsWith(string column, string value)
        {
            return EndsWith(column, value, false);
        }

        public static string EndsWith(string column, string value, bool ignoreCase)
        {
            return EndsWith(column, value, ignoreCase, true);
        }

        public static string EndsWith(string column, string value, bool ignoreCase, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return IsNull(column);
            }
            return string.Format(GetLikeFormat(ignoreCase, surround), column, EndsWith(value));
        }

        public static string Equals(string value)
        {
            return string.Format("{0}", Encode(value));
        }

        public static string Equals(string column, string value)
        {
            return Equals(column, value, false);
        }

        public static string Equals(string column, string value, bool ignoreCase)
        {
            return Equals(column, value, ignoreCase, true);
        }

        public static string Equals(string column, string value, bool ignoreCase, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return IsNull(column);
            }
            return string.Format(GetEqualFormat(ignoreCase, surround), column, Equals(value));
        }

        public static string GetEqualFormat(bool ignoreCase)
        {
            return GetEqualFormat(ignoreCase, true);
        }

        public static string GetEqualFormat(bool ignoreCase, bool surround)
        {
            if (surround)
            {
                return (ignoreCase ? "UPPER({0}) = UPPER('{1}')" : "{0} = '{1}'");
            }
            return (ignoreCase ? "UPPER({0}) = UPPER({1})" : "{0} = {1}");
        }

        public static string GetLikeFormat(bool ignoreCase)
        {
            return GetLikeFormat(ignoreCase, true);
        }

        public static string GetLikeFormat(bool ignoreCase, bool surround)
        {
            if (surround)
            {
                return (ignoreCase ? "UPPER({0}) LIKE UPPER('{1}')" : "{0} LIKE '{1}'");
            }
            return (ignoreCase ? "UPPER({0}) LIKE UPPER({1})" : "{0} LIKE {1}");
        }

        public static string IsNotNull(string column)
        {
            return string.Format("{0} IS NOT NULL", column);
        }

        public static string IsNull(string column)
        {
            return string.Format("{0} IS NULL", column);
        }

        public static string Like(string value)
        {
            return string.Format("{0}", Encode(value));
        }

        public static string Like(string column, string value)
        {
            return Like(column, value, false);
        }

        public static string Like(string column, string value, bool ignoreCase)
        {
            return Like(column, value, ignoreCase, true);
        }

        public static string Like(string column, string value, bool ignoreCase, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return IsNull(column);
            }
            return string.Format(GetLikeFormat(ignoreCase, surround), column, Like(value));
        }

        public static string StartsWith(string value)
        {
            return string.Format("{0}%", Encode(value));
        }

        public static string StartsWith(string column, string value)
        {
            return StartsWith(column, value, false);
        }

        public static string StartsWith(string column, string value, bool ignoreCase)
        {
            return StartsWith(column, value, ignoreCase, true);
        }

        public static string StartsWith(string column, string value, bool ignoreCase, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return IsNull(column);
            }
            return string.Format(GetLikeFormat(ignoreCase, surround), column, StartsWith(value));
        }
    }
}

