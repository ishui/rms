namespace RmsPM.DAL.QueryStrategy
{
    using System;

    public class SystemClassItem
    {
        private string m_ClassCode;
        private string m_CreateUserColumnName;
        private string m_KeyColumnName;
        private string m_TableName;
        private string m_TypeColumnName;

        public SystemClassItem()
        {
            this.m_ClassCode = "";
            this.m_TableName = "";
            this.m_KeyColumnName = "";
            this.m_TypeColumnName = "";
            this.m_CreateUserColumnName = "";
        }

        public SystemClassItem(string classCode, string tableName, string keyColumnName, string typeColumnName, string createUserColumnName)
        {
            this.m_ClassCode = "";
            this.m_TableName = "";
            this.m_KeyColumnName = "";
            this.m_TypeColumnName = "";
            this.m_CreateUserColumnName = "";
            this.m_ClassCode = classCode;
            this.m_TableName = tableName;
            this.m_KeyColumnName = keyColumnName;
            this.m_TypeColumnName = typeColumnName;
            this.m_CreateUserColumnName = createUserColumnName;
        }

        public string ClassCode
        {
            get
            {
                return this.m_ClassCode;
            }
            set
            {
                this.m_ClassCode = value;
            }
        }

        public string CreateUserColumnName
        {
            get
            {
                return this.m_CreateUserColumnName;
            }
            set
            {
                this.m_CreateUserColumnName = value;
            }
        }

        public string KeyColumnName
        {
            get
            {
                return this.m_KeyColumnName;
            }
            set
            {
                this.m_KeyColumnName = value;
            }
        }

        public string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
            }
        }

        public string TypeColumnName
        {
            get
            {
                return this.m_TypeColumnName;
            }
            set
            {
                this.m_TypeColumnName = value;
            }
        }
    }
}

