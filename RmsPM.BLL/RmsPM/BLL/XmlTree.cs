namespace RmsPM.BLL
{
    using System;
    using System.Data;

    public class XmlTree
    {
        public static string GetDataToXmlString(DataTable tb)
        {
            string text3;
            try
            {
                string text = "<?xml version=\"1.0\"?>";
                text = text + "<" + tb.TableName + "s>";
                foreach (DataRow row in tb.Rows)
                {
                    text = text + "<" + tb.TableName + ">";
                    foreach (DataColumn column in tb.Columns)
                    {
                        string text2 = ConvertRule.ToString(row[column]).Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
                        text = text + string.Format("<{0}>{1}</{0}>", column.ColumnName, text2);
                    }
                    text = text + "</" + tb.TableName + ">";
                }
                text3 = text + "</" + tb.TableName + "s>";
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }
    }
}

