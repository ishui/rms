namespace Rms.ORMap
{
    using System;
    using System.Data;
    using System.Xml;

    public abstract class AbstractClassBuilder : IClassBuilder
    {
        public abstract EntityData BuildClass(string className);
        protected virtual DataTable BuildTable(XmlNode tablenode)
        {
            DataTable table2;
            try
            {
                XmlNodeList list = tablenode.SelectNodes("Column");
                int count = list.Count;
                if (count == 0)
                {
                    throw new ApplicationException("没有定义字段");
                }
                DataTable table = new DataTable(tablenode.SelectSingleNode("Name").InnerText);
                int num2 = tablenode.SelectNodes("Column[IsKey='true']").Count;
                DataColumn[] columnArray = new DataColumn[num2];
                int index = 0;
                for (int i = 0; i < count; i++)
                {
                    DataColumn column = new DataColumn(list[i].SelectSingleNode("Name").InnerText);
                    string typeName = list[i].SelectSingleNode("DataType").InnerText;
                    column.DataType = Type.GetType(typeName);
                    if ("System.String" == typeName)
                    {
                        column.MaxLength = int.Parse(list[i].SelectSingleNode("ColumnSize").InnerText);
                    }
                    if ("true" == list[i].SelectSingleNode("IsKey").InnerText)
                    {
                        columnArray[index] = column;
                        index++;
                    }
                    table.Columns.Add(column);
                }
                if (num2 > 0)
                {
                    table.PrimaryKey = columnArray;
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public virtual SqlStruct GetSqlStruct(string className, string sqlStringName)
        {
            return SqlManager.GetSqlStruct(className, className, sqlStringName);
        }

        protected virtual XmlNode GetXmlRoot(string className)
        {
            return XmlDefineFileManage.GetXmlDoc(className).DocumentElement;
        }
    }
}

