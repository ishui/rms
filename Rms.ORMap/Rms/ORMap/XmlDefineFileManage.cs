namespace Rms.ORMap
{
    using System;
    using System.IO;
    using System.Xml;

    public sealed class XmlDefineFileManage
    {
        private XmlDefineFileManage()
        {
        }

        /// <summary>
        /// 读取XML文件
        /// </summary>
        /// <param name="fileName">XML文件名</param>
        /// <returns></returns>
        public static XmlDocument GetXmlDoc(string fileName)
        {
            XmlDocument document2;
            try
            {
                string path = ApplicationConfiguration.EntityDefinePath + fileName + ".xml";
                if (!File.Exists(path))
                {
                    throw new ApplicationException(path + " 文件不存在");
                }
                XmlDocument document = new XmlDocument();
                document.Load(path);
                document2 = document;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return document2;
        }
    }
}

