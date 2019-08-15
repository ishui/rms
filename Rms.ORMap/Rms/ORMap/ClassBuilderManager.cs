namespace Rms.ORMap
{
    using System;
    using System.Collections;
    using System.Xml;

    /// <summary>
    /// EntityData对象的容器,对象池,并保持EntityData对象的单个实例
    /// </summary>
    public sealed class ClassBuilderManager
    {
        private static Hashtable builderNames = new Hashtable();

        public static string GetClassBuilderName(string className)
        {
            string text;
            try
            {
                if (!builderNames.Contains(className))
                {
                    throw new ApplicationException("没有在ClassBuilders.xml中定义" + className);
                }
                text = (string) builderNames[className];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        /// <summary>
        /// 程序启动时初始化实体类列表,Global文件中调用
        /// </summary>
        public static void LoadClassBuilderDefine()
        {
            try
            {
                XmlNodeList list = XmlDefineFileManage.GetXmlDoc("ClassBuilders").DocumentElement.SelectNodes("Class");
                int count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    XmlNode node = list.Item(i);
                    string key = node.Attributes["Name"].InnerText;
                    string innerText = node.Attributes["Type"].InnerText;
                    if (builderNames.Contains(key))
                    {
                        throw new ApplicationException("在ClassBuilders.xml中有重复定义: " + key);
                    }
                    builderNames.Add(key, innerText);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

