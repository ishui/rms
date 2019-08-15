namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class EntityUtil
    {
        public static void Add(IList list, object item)
        {
            if ((list != null) && (item != null))
            {
                list.Add(item);
            }
        }

        public static object ChangeGenericType(object value, Type conversionType)
        {
            return ChangeGenericType(value, conversionType, true);
        }

        public static object ChangeGenericType(object value, Type conversionType, bool convertBlankToNull)
        {
            object obj2 = null;
            if (conversionType.IsGenericType)
            {
                Type typeDefinition = conversionType.GetGenericTypeDefinition();
                Type[] typeArguments = conversionType.GetGenericArguments();
                if (typeArguments.Length == 1)
                {
                    Type type2 = typeArguments[0];
                    object obj3 = ChangeType(value, type2, convertBlankToNull);
                    obj2 = GetNewGenericEntity(typeDefinition, typeArguments, new object[] { obj3 });
                }
            }
            return obj2;
        }

        public static object ChangeType(object value, Type conversionType)
        {
            return ChangeType(value, conversionType, true);
        }

        public static object ChangeType(object value, Type conversionType, bool convertBlankToNull)
        {
            object obj2 = null;
            if (((convertBlankToNull && (value != null)) && (value is string)) && string.IsNullOrEmpty(value.ToString().Trim()))
            {
                value = null;
            }
            if (conversionType.IsGenericType)
            {
                return ChangeGenericType(value, conversionType, convertBlankToNull);
            }
            if (value == null)
            {
                return obj2;
            }
            if (!(value is IConvertible))
            {
                if (conversionType == typeof(byte[]))
                {
                    obj2 = value;
                }
                else
                {
                    value = value.ToString();
                }
            }
            if (conversionType == typeof(Guid))
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    obj2 = new Guid(value.ToString());
                }
                return obj2;
            }
            return Convert.ChangeType(value, conversionType);
        }

        public static ConstructorInfo GetConstructor(Type type)
        {
            return GetConstructor(type, null);
        }

        public static ConstructorInfo GetConstructor(Type type, Type[] types)
        {
            if (type == null)
            {
                return null;
            }
            return type.GetConstructor(types ?? Type.EmptyTypes);
        }

        public static object GetEntity(IEnumerable entityList, int index)
        {
            IList list = GetEntityList(entityList);
            object obj2 = null;
            if (list.Count > index)
            {
                obj2 = list[index];
            }
            return obj2;
        }

        public static object GetEntity(IList entities, string propertyName, object propertyValue)
        {
            if (HasEntities(entities))
            {
                foreach (object obj2 in entities)
                {
                    if (IsPropertyValueEqual(obj2, propertyName, propertyValue))
                    {
                        return obj2;
                    }
                }
            }
            return null;
        }

        public static IList GetEntityList(object entityList)
        {
            IList list = null;
            if (entityList == null)
            {
                return new ArrayList();
            }
            if (entityList is IList)
            {
                return (IList) entityList;
            }
            list = new ArrayList();
            if (entityList is IEnumerable)
            {
                IEnumerable enumerable = entityList as IEnumerable;
                foreach (object obj2 in enumerable)
                {
                    if (obj2 != null)
                    {
                        list.Add(obj2);
                    }
                }
                return list;
            }
            list.Add(entityList);
            return list;
        }

        public static IList GetEntityList(object entity, string propertyName)
        {
            return GetEntityList(GetPropertyValue(entity, propertyName));
        }

        public static MethodInfo GetMethod(object item, string methodName)
        {
            return GetMethod(item, methodName, null);
        }

        public static MethodInfo GetMethod(Type type, string methodName)
        {
            return GetMethod(type, methodName, null);
        }

        public static MethodInfo GetMethod(object item, string methodName, params Type[] types)
        {
            MethodInfo info = null;
            if (item != null)
            {
                info = GetMethod(item.GetType(), methodName, types);
            }
            return info;
        }

        public static MethodInfo GetMethod(Type type, string methodName, params Type[] types)
        {
            if ((type == null) || string.IsNullOrEmpty(methodName))
            {
                return null;
            }
            return type.GetMethod(methodName, types ?? Type.EmptyTypes);
        }

        public static object GetNewEntity(Type type)
        {
            return GetNewEntity(type, null);
        }

        public static object GetNewEntity(Type type, params object[] args)
        {
            ConstructorInfo constructor = GetConstructor(type, GetTypes(args));
            return ((constructor != null) ? constructor.Invoke(args) : null);
        }

        public static object GetNewGenericEntity(Type genericType)
        {
            return GetNewGenericEntity(genericType, null, new object[0]);
        }

        public static object GetNewGenericEntity(Type genericType, params object[] args)
        {
            if (genericType == null)
            {
                return null;
            }
            if (((args != null) && (args.Length == 1)) && (args[0] == null))
            {
                args = null;
            }
            return Activator.CreateInstance(genericType, args);
        }

        public static object GetNewGenericEntity(Type typeDefinition, Type[] typeArguments, params object[] args)
        {
            return GetNewGenericEntity(MakeGenericType(typeDefinition, typeArguments), args);
        }

        public static PropertyInfo GetProperty(object item, string propertyName)
        {
            PropertyInfo property = null;
            if (item != null)
            {
                property = GetProperty(item.GetType(), propertyName);
            }
            return property;
        }

        public static PropertyInfo GetProperty(Type type, string propertyName)
        {
            PropertyInfo property = null;
            if (!((type == null) || string.IsNullOrEmpty(propertyName)))
            {
                property = type.GetProperty(propertyName);
            }
            return property;
        }

        public static object GetPropertyValue(object item, string propertyName)
        {
            PropertyInfo property = null;
            return GetPropertyValue(item, propertyName, out property);
        }

        public static object GetPropertyValue(object item, string propertyName, out PropertyInfo property)
        {
            object obj2 = null;
            property = GetProperty(item, propertyName);
            if ((property != null) && property.CanRead)
            {
                obj2 = property.GetValue(item, null);
            }
            return obj2;
        }

        public static object GetStaticPropertyValue(Type type, string propertyName)
        {
            PropertyInfo property = null;
            return GetStaticPropertyValue(type, propertyName, out property);
        }

        public static object GetStaticPropertyValue(Type type, string propertyName, out PropertyInfo property)
        {
            object obj2 = null;
            property = GetProperty(type, propertyName);
            if ((property != null) && property.CanRead)
            {
                obj2 = property.GetValue(null, null);
            }
            return obj2;
        }

        public static Type GetType(string typeName)
        {
            Type type = null;
            if (!string.IsNullOrEmpty(typeName))
            {
                type = Type.GetType(typeName, true);
            }
            return type;
        }

        public static string GetTypeNames(params Type[] types)
        {
            StringBuilder builder = new StringBuilder();
            foreach (Type type in types)
            {
                if (builder.Length > 0)
                {
                    builder.Append(", ");
                }
                builder.Append(type.Name);
            }
            return builder.ToString();
        }

        public static Type[] GetTypes(params object[] args)
        {
            Type[] emptyTypes = Type.EmptyTypes;
            if (args != null)
            {
                emptyTypes = Type.GetTypeArray(args);
            }
            return emptyTypes;
        }

        public static bool GuidTryParse(string s, out Guid result)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            Regex regex = new Regex(@"^[A-Fa-f0-9]{32}$|^({|\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\))?$|^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
            if (regex.Match(s).Success)
            {
                result = new Guid(s);
                return true;
            }
            result = Guid.Empty;
            return false;
        }

        public static bool HasEntities(IList entities)
        {
            return ((entities != null) && (entities.Count > 0));
        }

        public static void InitEntityDateTimeValues(object entity, params string[] propertyNames)
        {
            if ((entity != null) && (propertyNames != null))
            {
                foreach (string text in propertyNames)
                {
                    PropertyInfo property = GetProperty(entity, text);
                    if (((property != null) && property.CanWrite) && property.PropertyType.IsAssignableFrom(typeof(DateTime)))
                    {
                        property.SetValue(entity, DateTime.Now, null);
                    }
                }
            }
        }

        public static object InvokeMethod(object entity, string methodName)
        {
            return InvokeMethod(entity, methodName, null, null);
        }

        public static object InvokeMethod(object entity, string methodName, object[] args)
        {
            return InvokeMethod(entity, methodName, args, GetTypes(args));
        }

        public static object InvokeMethod(object entity, string methodName, object[] args, Type[] types)
        {
            MethodInfo info = GetMethod(entity, methodName, types);
            if (info == null)
            {
                string format = "The method '{0}' with arguments '{1}' could not be located on the specified entity.";
                string text2 = (types == null) ? "()" : ("(" + GetTypeNames(types) + ")");
                throw new ArgumentException(string.Format(format, methodName, text2));
            }
            return info.Invoke(entity, args);
        }

        public static bool IsPropertyValueEqual(object item, string propertyName, object propertyValue)
        {
            PropertyInfo property = null;
            object objA = GetPropertyValue(item, propertyName, out property);
            object objB = null;
            bool flag = false;
            if (property != null)
            {
                objB = ChangeType(propertyValue, property.PropertyType);
                flag = object.Equals(objA, objB);
            }
            return flag;
        }

        public static Type MakeGenericType(Type typeDefinition, Type[] typeArguments)
        {
            Type type = null;
            if (((typeDefinition != null) && (typeArguments != null)) && (typeArguments.Length > 0))
            {
                type = typeDefinition.MakeGenericType(typeArguments);
            }
            return type;
        }

        public static void Remove(IList list, object item)
        {
            if ((list != null) && (item != null))
            {
                if (item is IEntity)
                {
                    ((IEntity) item).MarkToDelete();
                }
                list.Remove(item);
            }
        }

        public static Guid SetEntityKeyValue(object entity, string entityKeyName)
        {
            PropertyInfo property = null;
            object obj2 = GetPropertyValue(entity, entityKeyName, out property);
            Guid empty = Guid.Empty;
            if (((property != null) && property.PropertyType.IsAssignableFrom(typeof(Guid))) && (Guid.Empty.Equals(obj2) && property.CanWrite))
            {
                empty = Guid.NewGuid();
                property.SetValue(entity, empty, null);
            }
            return empty;
        }

        public static void SetEntityValues(object entity, IDictionary values)
        {
            if ((entity != null) && (values != null))
            {
                foreach (object obj3 in values.Keys)
                {
                    if (obj3 is string)
                    {
                        object propertyValue = values[obj3];
                        SetPropertyValue(entity, obj3.ToString(), propertyValue);
                    }
                }
            }
        }

        public static void SetPropertyValue(object item, string propertyName, object propertyValue)
        {
            SetPropertyValue(item, propertyName, propertyValue, true);
        }

        public static void SetPropertyValue(object item, string propertyName, object propertyValue, bool convertBlankToNull)
        {
            PropertyInfo property = null;
            SetPropertyValue(item, propertyName, propertyValue, out property, convertBlankToNull);
        }

        public static void SetPropertyValue(object item, string propertyName, object propertyValue, out PropertyInfo property)
        {
            SetPropertyValue(item, propertyName, propertyValue, out property, true);
        }

        public static void SetPropertyValue(object item, string propertyName, object propertyValue, out PropertyInfo property, bool convertBlankToNull)
        {
            property = GetProperty(item, propertyName);
            if ((property != null) && property.CanWrite)
            {
                object obj2 = ChangeType(propertyValue, property.PropertyType, convertBlankToNull);
                property.SetValue(item, obj2, null);
            }
        }
    }
}

