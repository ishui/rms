namespace Rms.ORMap
{
    using System;

    public sealed class ApplicationConfiguration
    {
        private static string m_DBConnectionString;
        private static string m_DBType;
        private static string m_EntityDefinePath;

        private ApplicationConfiguration()
        {
        }

        public static void SetAppConfiguration(IAppConfigSetter setter)
        {
            m_DBConnectionString = setter.GetDBConnectionString();
            m_EntityDefinePath = setter.GetEntityDefinePath();
            m_DBType = setter.GetDBType();
        }

        public static string DBConnectionString
        {
            get
            {
                return m_DBConnectionString;
            }
        }

        public static string DBType
        {
            get
            {
                return m_DBType;
            }
        }

        /// <summary>
        /// 得到EntityDefinePath里的内容
        /// </summary>
        public static string EntityDefinePath
        {
            get
            {
                return m_EntityDefinePath;
            }
        }
    }
}

