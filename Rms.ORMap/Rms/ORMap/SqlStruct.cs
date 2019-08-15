namespace Rms.ORMap
{
    using System;
    using System.Text;

    public class SqlStruct
    {
        public string[] ColumnsList;
        public string CommandType;
        public string[] OrderNameList;
        public string[] OrderSortList;
        public string[] ParamsList;
        public string[] SqlDbTypeList;
        public string SqlString;

        public string GetSqlStringWithOrder()
        {
            StringBuilder builder = new StringBuilder("");
            int length = this.OrderNameList.Length;
            for (int i = 0; i < length; i++)
            {
                if (builder.ToString().Length > 0)
                {
                    builder.Append(", ");
                }
                else
                {
                    builder.Append(" Order By ");
                }
                builder.Append(" " + this.OrderNameList[i] + " " + this.OrderSortList[i] + " ");
            }
            return (this.SqlString + builder.ToString());
        }

        public string GetSqlStringWithParams(string[] values)
        {
            string sqlString = this.SqlString;
            int length = this.ColumnsList.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                sqlString.Replace(this.ParamsList[i], values[i]);
            }
            return sqlString;
        }
    }
}

