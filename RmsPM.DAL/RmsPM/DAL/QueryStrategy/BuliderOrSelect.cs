namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using System.Collections;
    using System.Text;

    public class BuliderOrSelect
    {
        protected string _TableName;
        protected ArrayList als;
        public RmsPM.DAL.QueryStrategy.ColumnMessage ColumnMessage;

        public BuliderOrSelect()
        {
            this.als = new ArrayList();
        }

        public BuliderOrSelect(string TableName)
        {
            this.als = new ArrayList();
        }

        public string GetOrSqlString(string tableName, string tableColumn, string[] NameList)
        {
            int length = NameList.Length;
            StringBuilder builder = new StringBuilder("Select * From " + tableName + " Where ");
            for (int i = 0; i < length; i++)
            {
                if (i == (length - 1))
                {
                    builder.Append("tableColumn= " + NameList[i]);
                    break;
                }
                builder.Append("tableColumn= " + NameList[i] + " or ");
            }
            return builder.ToString();
        }
    }
}

