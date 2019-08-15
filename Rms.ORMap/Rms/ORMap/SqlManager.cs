namespace Rms.ORMap
{
    using System;
    using System.Xml;

    public sealed class SqlManager
    {
        private SqlManager()
        {
        }

        public static SqlStruct GetSqlStruct(string className, string sqlStringName)
        {
            return GetSqlStruct(className, className, sqlStringName);
        }

        public static SqlStruct GetSqlStruct(string className, string tableName, string sqlStringName)
        {
            SqlStruct struct3;
            try
            {
                XmlDocument xmlDoc = XmlDefineFileManage.GetXmlDoc(className);
                SqlStruct struct2 = new SqlStruct();
                XmlNode node2 = xmlDoc.DocumentElement.SelectSingleNode("Table[Name='" + tableName + "']");
                if (node2 == null)
                {
                    throw new ApplicationException("没有找到表: " + tableName);
                }
                XmlNode node3 = node2.SelectSingleNode("Sql[Name='" + sqlStringName + "']");
                if (node3 == null)
                {
                    throw new ApplicationException("没有找到定义的字符串:  " + tableName + "---" + sqlStringName);
                }
                struct2.SqlString = node3.SelectSingleNode("String").InnerText;
                struct2.CommandType = node3.SelectSingleNode("CommandType").InnerText;
                XmlNodeList list = node3.SelectNodes("Param");
                int count = list.Count;
                struct2.ParamsList = new string[count];
                struct2.ColumnsList = new string[count];
                struct2.SqlDbTypeList = new string[count];
                if (count != 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        struct2.ParamsList[i] = list[i].SelectSingleNode("Name").InnerText;
                        struct2.ColumnsList[i] = list[i].SelectSingleNode("Column").InnerText;
                        struct2.SqlDbTypeList[i] = list[i].SelectSingleNode("SqlDbType").InnerText;
                    }
                }
                XmlNodeList list2 = node3.SelectNodes("Order");
                int num3 = list2.Count;
                struct2.OrderNameList = new string[num3];
                struct2.OrderSortList = new string[num3];
                for (int j = 0; j < num3; j++)
                {
                    struct2.OrderNameList[j] = list2[j].SelectSingleNode("Name").InnerText;
                    struct2.OrderSortList[j] = list2[j].SelectSingleNode("Sort").InnerText;
                }
                struct3 = struct2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return struct3;
        }
    }
}

