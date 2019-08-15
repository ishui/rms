namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public static class EntityHelper
    {
        public static T Clone<T>(T sourceEntity)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream();
            formatter.Serialize(serializationStream, sourceEntity);
            serializationStream.Seek((long) 0, SeekOrigin.Begin);
            return (T) formatter.Deserialize(serializationStream);
        }

        public static object DeserializeBinary(byte[] bytes)
        {
            MemoryStream serializationStream = new MemoryStream(bytes);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(serializationStream);
        }

        public static T DeserializeEntityXml<T>(string data) where T: IEntity
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StringReader(data);
            T local = (T) serializer.Deserialize(textReader);
            textReader.Close();
            return local;
        }

        public static TList<T> DeserializeListXml<T>(string data) where T: IEntity, new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TList<T>));
            TextReader textReader = new StringReader(data);
            TList<T> list = (TList<T>) serializer.Deserialize(textReader);
            textReader.Close();
            return list;
        }

        public static VList<T> DeserializeVListXml<T>(string data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(VList<T>));
            TextReader textReader = new StringReader(data);
            VList<T> list = (VList<T>) serializer.Deserialize(textReader);
            textReader.Close();
            return list;
        }

        public static object DeserializeXml(string root, Type type, XmlReader reader)
        {
            XmlRootAttribute attribute = new XmlRootAttribute();
            attribute.ElementName = root;
            XmlSerializer serializer = new XmlSerializer(type, attribute);
            return serializer.Deserialize(reader);
        }

        public static T GetAttribute<T>(Enum e) where T: Attribute
        {
            T local = default(T);
            MemberInfo[] member = e.GetType().GetMember(e.ToString());
            if ((member != null) && (member.Length == 1))
            {
                object[] customAttributes = member[0].GetCustomAttributes(typeof(T), false);
                if (customAttributes.Length > 0)
                {
                    local = (T) customAttributes[0];
                }
            }
            return local;
        }

        public static PropertyDescriptorCollection GetBindableProperties(Type type)
        {
            Attribute[] attributes = new Attribute[] { new TiannuoPM.Entities.BindableAttribute() };
            return TypeDescriptor.GetProperties(type, attributes);
        }

        public static long GetByteLength(object obj)
        {
            MemoryStream serializationStream = new MemoryStream();
            new BinaryFormatter().Serialize(serializationStream, obj);
            return serializationStream.Length;
        }

        public static string GetEnumTextValue(Enum e)
        {
            string text = "";
            MemberInfo[] member = e.GetType().GetMember(e.ToString());
            if ((member != null) && (member.Length == 1))
            {
                object[] customAttributes = member[0].GetCustomAttributes(typeof(EnumTextValueAttribute), false);
                if (customAttributes.Length == 1)
                {
                    text = ((EnumTextValueAttribute) customAttributes[0]).Text;
                }
            }
            return text;
        }

        public static object GetEnumValue(string text, Type enumType)
        {
            MemberInfo[] members = enumType.GetMembers();
            foreach (MemberInfo info in members)
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(EnumTextValueAttribute), false);
                if ((customAttributes.Length == 1) && (((EnumTextValueAttribute) customAttributes[0]).Text == text))
                {
                    return Enum.Parse(enumType, info.Name);
                }
            }
            throw new ArgumentOutOfRangeException("text", text, "The text passed does not correspond to an attributed enum value");
        }

        public static byte[] SerializeBinary(IList entityCollection)
        {
            MemoryStream serializationStream = new MemoryStream();
            new BinaryFormatter().Serialize(serializationStream, entityCollection);
            return serializationStream.ToArray();
        }

        public static byte[] SerializeBinary(IEntity entity)
        {
            MemoryStream serializationStream = new MemoryStream();
            new BinaryFormatter().Serialize(serializationStream, entity);
            return serializationStream.ToArray();
        }

        public static void SerializeBinary(IList entityCollection, string path)
        {
            FileStream serializationStream = new FileStream(path, FileMode.OpenOrCreate);
            new BinaryFormatter().Serialize(serializationStream, entityCollection);
            serializationStream.Close();
        }

        public static void SerializeBinary(IEntity entity, string path)
        {
            FileStream serializationStream = new FileStream(path, FileMode.OpenOrCreate);
            new BinaryFormatter().Serialize(serializationStream, entity);
            serializationStream.Close();
        }

        public static string SerializeXml<T>(VList<T> entityCollection)
        {
            XmlSerializer serializer = new XmlSerializer(entityCollection.GetType());
            StringBuilder sb = new StringBuilder();
            TextWriter textWriter = new StringWriter(sb);
            serializer.Serialize(textWriter, entityCollection);
            textWriter.Close();
            return sb.ToString();
        }

        public static string SerializeXml(IEntity entity)
        {
            XmlSerializer serializer = new XmlSerializer(entity.GetType());
            StringBuilder sb = new StringBuilder();
            TextWriter textWriter = new StringWriter(sb);
            serializer.Serialize(textWriter, entity);
            textWriter.Close();
            return sb.ToString();
        }

        public static string SerializeXml<T>(TList<T> entityCollection) where T: IEntity, new()
        {
            XmlSerializer serializer = new XmlSerializer(entityCollection.GetType());
            StringBuilder sb = new StringBuilder();
            TextWriter textWriter = new StringWriter(sb);
            serializer.Serialize(textWriter, entityCollection);
            textWriter.Close();
            return sb.ToString();
        }

        public static void SerializeXml(IEntity entity, string path)
        {
            XmlSerializer serializer = new XmlSerializer(entity.GetType());
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize((TextWriter) writer, entity);
            writer.Close();
        }

        public static void SerializeXml<T>(TList<T> entityCollection, string path) where T: IEntity, new()
        {
            XmlSerializer serializer = new XmlSerializer(entityCollection.GetType());
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize((TextWriter) writer, entityCollection);
            writer.Close();
        }

        public static void SerializeXml<T>(VList<T> entityCollection, string path)
        {
            XmlSerializer serializer = new XmlSerializer(entityCollection.GetType());
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize((TextWriter) writer, entityCollection);
            writer.Close();
        }

        public static string ToString(object o)
        {
            StringBuilder builder = new StringBuilder();
            Type type = o.GetType();
            PropertyInfo[] properties = type.GetProperties();
            builder.Append("Properties for: " + o.GetType().Name + Environment.NewLine);
            foreach (PropertyInfo info in properties)
            {
                try
                {
                    builder.Append("\t" + info.Name + "(" + info.PropertyType.ToString() + "): ");
                    if (null != info.GetValue(o, null))
                    {
                        builder.Append(info.GetValue(o, null).ToString());
                    }
                }
                catch
                {
                }
                builder.Append(Environment.NewLine);
            }
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo info2 in fields)
            {
                try
                {
                    builder.Append("\t" + info2.Name + "(" + info2.FieldType.ToString() + "): ");
                    if (null != info2.GetValue(o))
                    {
                        builder.Append(info2.GetValue(o).ToString());
                    }
                }
                catch
                {
                }
                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }

        public static T XmlDeserialize<T>(string xmlData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StringReader(xmlData);
            T local = (T) serializer.Deserialize(textReader);
            textReader.Close();
            return local;
        }

        public static string XmlSerialize<T>(T item)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            serializer.Serialize((TextWriter) writer, item);
            writer.Close();
            return sb.ToString();
        }
    }
}

