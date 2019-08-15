namespace Rms.ORMap
{
    using System;
    using System.Xml;

    public class SingleTableClassBuilder : AbstractClassBuilder
    {
        public override EntityData BuildClass(string className)
        {
            EntityData data2;
            try
            {
                EntityData data = new EntityData();
                data.ClassName = className;
                data.MainTableName = className;
                data.EntityTypeName = "Single";
                XmlNode tablenode = this.GetXmlRoot(className).SelectSingleNode("Table");
                data.Tables.Add(this.BuildTable(tablenode));
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

