namespace Rms.ORMap
{
    using System;

    public sealed class DBCommonBuilder
    {
        public static IDBCommon BuildDBCommon()
        {
            string dBConnectionString = ApplicationConfiguration.DBConnectionString;
            return BuildDBCommon(ApplicationConfiguration.DBType, dBConnectionString);
        }

        public static IDBCommon BuildDBCommon(string m_DBType, string m_DBConnectionString)
        {
            IDBCommon common = null;
            string str = m_DBType.ToUpper();
            if (str != null)
            {
                str = string.IsInterned(str);
                if ((str != "OLEDB") && (str == "SQLCLIENT"))
                {
                    common = new SqlClientDBCommon(m_DBConnectionString);
                }
            }
            return common;
        }
    }
}

