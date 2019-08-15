namespace TiannuoPM.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Text;
    using System.Text.RegularExpressions;
    using TiannuoPM.Entities;

    public sealed class Utility
    {
        private static readonly Regex regSystemThreats = new Regex(@"\s?;\s?|\s?drop\s|\s?grant\s|^'|\s?--|\s?union\s|\s?delete\s|\s?truncate\s|\s?sysobjects\s?|\s?xp_.*?|\s?syslogins\s?|\s?sysremote\s?|\s?sysusers\s?|\s?sysxlogins\s?|\s?sysdatabases\s?|\s?aspnet_.*?|\s?exec\s?|", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private Utility()
        {
        }

        public static DataSet ConvertDataReaderToDataSet(IDataReader reader)
        {
            DataSet set = new DataSet();
            do
            {
                DataRow row;
                DataColumn column;
                DataTable schemaTable = reader.GetSchemaTable();
                DataTable table = new DataTable();
                if (schemaTable != null)
                {
                    int i;
                    for (i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        row = schemaTable.Rows[i];
                        string columnName = (string) row["ColumnName"];
                        column = new DataColumn(columnName, (Type) row["DataType"]);
                        table.Columns.Add(column);
                    }
                    set.Tables.Add(table);
                    while (reader.Read())
                    {
                        row = table.NewRow();
                        for (i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader.GetValue(i);
                        }
                        table.Rows.Add(row);
                    }
                }
                else
                {
                    column = new DataColumn("RowsAffected");
                    table.Columns.Add(column);
                    set.Tables.Add(table);
                    row = table.NewRow();
                    row[0] = reader.RecordsAffected;
                    table.Rows.Add(row);
                }
            }
            while (reader.NextResult());
            return set;
        }

        public static object DefaultToDBNull(object val, DbType dbtype)
        {
            if ((val == null) || object.Equals(val, GetDefaultByType(dbtype)))
            {
                return DBNull.Value;
            }
            return val;
        }

        public static bool DetectSqlInjection(string whereClause)
        {
            return regSystemThreats.IsMatch(whereClause);
        }

        public static bool DetectSqlInjection(string whereClause, string orderBy)
        {
            return (regSystemThreats.IsMatch(whereClause) || regSystemThreats.IsMatch(orderBy));
        }

        public static DataSet ExecuteDataSet(Database database, DbCommand dbCommand)
        {
            DataSet set = null;
            try
            {
                set = database.ExecuteDataSet(dbCommand);
            }
            catch (Exception)
            {
                throw;
            }
            return set;
        }

        public static DataSet ExecuteDataSet(TransactionManager transactionManager, DbCommand dbCommand)
        {
            if (!transactionManager.IsOpen)
            {
                throw new DataException("Transaction must be open before executing a query.");
            }
            DataSet set = null;
            try
            {
                set = transactionManager.Database.ExecuteDataSet(dbCommand, transactionManager.TransactionObject);
            }
            catch (Exception)
            {
                throw;
            }
            return set;
        }

        public static int ExecuteNonQuery(Database database, DbCommand dbCommand)
        {
            int num = 0;
            try
            {
                num = database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception)
            {
                throw;
            }
            return num;
        }

        public static int ExecuteNonQuery(TransactionManager transactionManager, DbCommand dbCommand)
        {
            if (!transactionManager.IsOpen)
            {
                throw new DataException("Transaction must be open before executing a query.");
            }
            int num = 0;
            try
            {
                num = transactionManager.Database.ExecuteNonQuery(dbCommand, transactionManager.TransactionObject);
            }
            catch (Exception)
            {
                throw;
            }
            return num;
        }

        public static IDataReader ExecuteReader(Database database, DbCommand dbCommand)
        {
            IDataReader reader = null;
            try
            {
                reader = database.ExecuteReader(dbCommand);
            }
            catch (Exception)
            {
                throw;
            }
            return reader;
        }

        public static IDataReader ExecuteReader(TransactionManager transactionManager, DbCommand dbCommand)
        {
            if (!transactionManager.IsOpen)
            {
                throw new DataException("Transaction must be open before executing a query.");
            }
            IDataReader reader = null;
            try
            {
                reader = transactionManager.Database.ExecuteReader(dbCommand, transactionManager.TransactionObject);
            }
            catch (Exception)
            {
                throw;
            }
            return reader;
        }

        public static object GetDataValue(IDataParameter p)
        {
            if (p.Value != DBNull.Value)
            {
                return p.Value;
            }
            return GetDefaultByType(p.DbType);
        }

        public static object GetDefaultByType(DbType dataType)
        {
            switch (dataType)
            {
                case DbType.AnsiString:
                    return string.Empty;

                case DbType.Binary:
                    return new byte[0];

                case DbType.Byte:
                    return (byte) 0;

                case DbType.Boolean:
                    return false;

                case DbType.Currency:
                    return 0M;

                case DbType.Date:
                    return DateTime.MinValue;

                case DbType.DateTime:
                    return DateTime.MinValue;

                case DbType.Decimal:
                    return 0M;

                case DbType.Double:
                    return 0f;

                case DbType.Guid:
                    return Guid.Empty;

                case DbType.Int16:
                    return (short) 0;

                case DbType.Int32:
                    return 0;

                case DbType.Int64:
                    return (long) 0;

                case DbType.Object:
                    return null;

                case DbType.Single:
                    return 0f;

                case DbType.String:
                    return string.Empty;

                case DbType.Time:
                    return DateTime.MinValue;

                case DbType.VarNumeric:
                    return 0;

                case DbType.AnsiStringFixedLength:
                    return string.Empty;

                case DbType.StringFixedLength:
                    return string.Empty;
            }
            return null;
        }

        public static T GetParameterValue<T>(IDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value)
            {
                return default(T);
            }
            return (T) parameter.Value;
        }

        public static string ParseSortExpression(Type columnEnum, string sortExpression)
        {
            string text = string.Empty;
            if (!string.IsNullOrEmpty(sortExpression))
            {
                string[] textArray = sortExpression.Split(new char[] { ',' });
                StringBuilder builder = new StringBuilder();
                foreach (string text4 in textArray)
                {
                    string[] textArray2 = text4.Trim().Split(new char[] { ' ' });
                    if ((textArray2.Length == 1) || (textArray2.Length == 2))
                    {
                        string text2 = textArray2[0];
                        try
                        {
                            Enum e = (Enum) Enum.Parse(columnEnum, text2, true);
                            string enumTextValue = EntityHelper.GetEnumTextValue(e);
                            if (builder.Length > 0)
                            {
                                builder.Append(", ");
                            }
                            builder.AppendFormat("[{0}]", enumTextValue);
                            if ((textArray2.Length > 1) && SqlUtil.DESC.Equals(textArray2[1], StringComparison.OrdinalIgnoreCase))
                            {
                                builder.AppendFormat(" {0}", SqlUtil.DESC);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                text = builder.ToString();
            }
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("[{0}]", Enum.GetValues(columnEnum).GetValue(0));
            }
            return text;
        }
    }
}

