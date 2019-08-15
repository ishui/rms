namespace Rms.ORMap
{
    using System;
    using System.Xml;

    public class StandardTableClassBuilder : AbstractClassBuilder
    {
        public override EntityData BuildClass(string className)
        {
            EntityData data2;
            try
            {
                string text = "";
                EntityData data = new EntityData();
                data.ClassName = className;
                data.EntityTypeName = "Standard";
                XmlNodeList list = this.GetXmlRoot(className).SelectNodes("Table");
                foreach (XmlNode node2 in list)
                {
                    string innerText = node2.SelectSingleNode("Name").InnerText;
                    XmlNode tablenode = this.GetXmlRoot(innerText).SelectSingleNode("Table");
                    data.Tables.Add(this.BuildTable(tablenode));
                    if (node2.SelectSingleNode("Type").InnerText == "Main")
                    {
                        data.MainTableName = innerText;
                        text = innerText;
                    }
                }
                foreach (XmlNode node5 in list)
                {
                    string text3 = node5.SelectSingleNode("Name").InnerText;
                    string text4 = node5.SelectSingleNode("Type").InnerText;
                    string text5 = "";
                    string text6 = "";
                    if ("Child" == text4)
                    {
                        text5 = node5.SelectSingleNode("ParentColumnName").InnerText;
                        text6 = node5.SelectSingleNode("ChildColumnName").InnerText;
                        data.Relations.Add(className + "-" + text3, data.Tables[text].Columns[text5], data.Tables[text3].Columns[text6]);
                        continue;
                    }
                    if ("Parent" == text4)
                    {
                        text5 = node5.SelectSingleNode("ParentColumnName").InnerText;
                        text6 = node5.SelectSingleNode("ChildColumnName").InnerText;
                        data.Relations.Add(text3 + "-" + className, data.Tables[text3].Columns[text5], data.Tables[text].Columns[text6]);
                    }
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }
    }
}

