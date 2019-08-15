namespace RmsPM.WebControls.TreeView
{
    using System;
    using System.Data;

    public class XmlTree
    {
        public static string GetDataToXmlString(DataTable m_Table)
        {
            string text = "<?xml version=\"1.0\"?>";
            text = text + "<" + m_Table.TableName + "s>";
            foreach (DataRow row in m_Table.Rows)
            {
                text = text + "<" + m_Table.TableName + ">";
                for (int i = 0; i < m_Table.Columns.Count; i++)
                {
                    string text3 = text;
                    text = text3 + "<" + m_Table.Columns[i].ColumnName + ">" + row[i].ToString() + "</" + m_Table.Columns[i].ColumnName + ">";
                }
                text = text + "</" + m_Table.TableName + ">";
            }
            return (text + "</" + m_Table.TableName + "s>");
        }
    }
}

