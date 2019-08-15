namespace TiannuoPM.Entities
{
    using System;
    using System.Reflection;

    public sealed class Filter<T, Entity> where T: ListBase<Entity>
    {
        public Filter()
        {
        }

        public Filter(T objToFilter, string filter)
        {
            this.ApplyFilter(objToFilter, filter);
        }

        public void ApplyFilter(T ObjectToFilter, string StrFilter)
        {
            if (ObjectToFilter == null)
            {
                throw new ArgumentNullException("ObjectToFilter");
            }
            if (string.IsNullOrEmpty(StrFilter))
            {
                throw new ArgumentNullException("StrFilter");
            }
            int count = ObjectToFilter.Count;
            Expressions expressions = new Expressions(StrFilter);
            Type type = typeof(Entity);
            PropertyInfo[] infoArray = new PropertyInfo[expressions.Count];
            for (int i = 0; i < expressions.Count; i++)
            {
                infoArray[i] = type.GetProperty(expressions.Item(i).PropertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (infoArray[i] == null)
                {
                    throw new Exception("property " + expressions.Item(i).PropertyName + " does not exist!");
                }
            }
            for (int j = 0; j <= (ObjectToFilter.Count - 1); j++)
            {
                object obj2 = ObjectToFilter[j];
                bool[] flagArray = new bool[expressions.Count];
                bool flag = true;
                for (int k = 0; k <= (expressions.Count - 1); k++)
                {
                    object objectPropertyValue = infoArray[k].GetValue(obj2, new object[0]);
                    if (objectPropertyValue == null)
                    {
                        flagArray[k] = this.IsOk(expressions.Item(k).Operator, expressions.Item(k).UserValue);
                    }
                    else
                    {
                        flagArray[k] = this.IsOk(objectPropertyValue, expressions.Item(k).Operator, expressions.Item(k).UserValue);
                    }
                    if (!flagArray[k])
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    ObjectToFilter.RemoveFilteredItem(j);
                    count--;
                    j--;
                }
            }
        }

        private string CorrectUserValue(string UserValue)
        {
            if (UserValue.Substring(0, 1) == "'")
            {
                UserValue = UserValue.Replace("'", "");
            }
            if (UserValue.Substring(0, 1) == "#")
            {
                UserValue = UserValue.Replace("#", "");
            }
            return UserValue;
        }

        private bool IsOk(string Operator, object UserValue)
        {
            bool flag = false;
            if ((UserValue.ToString().ToUpper() == "NULL") & (Operator == "="))
            {
                flag = true;
            }
            return flag;
        }

        private bool IsOk(bool ObjectPropertyValue, string Operator, bool UserValue)
        {
            bool flag = false;
            if (Operator != "=")
            {
                throw new Exception("The operator '" + Operator + "' does not match the type string !");
            }
            if (ObjectPropertyValue == UserValue)
            {
                flag = true;
            }
            return flag;
        }

        private bool IsOk(DateTime ObjectPropertyValue, string Operator, DateTime UserValue)
        {
            bool flag = false;
            if (Operator == "=")
            {
                if (ObjectPropertyValue == UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">")
            {
                if (ObjectPropertyValue > UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">=")
            {
                if (ObjectPropertyValue >= UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == "<")
            {
                if (ObjectPropertyValue < UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator != "<=")
            {
                throw new Exception("The operator '" + Operator + "' does not match the type DateTime !");
            }
            if (ObjectPropertyValue <= UserValue)
            {
                flag = true;
            }
            return flag;
        }

        private bool IsOk(decimal ObjectPropertyValue, string Operator, decimal UserValue)
        {
            bool flag = false;
            if (Operator == "=")
            {
                if (ObjectPropertyValue == UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">")
            {
                if (ObjectPropertyValue > UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">=")
            {
                if (ObjectPropertyValue >= UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == "<")
            {
                if (ObjectPropertyValue < UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator != "<=")
            {
                throw new Exception("The operator '" + Operator + "' does not match the type decimal !");
            }
            if (ObjectPropertyValue <= UserValue)
            {
                flag = true;
            }
            return flag;
        }

        private bool IsOk(decimal ObjectPropertyValue, string Operator, int UserValue)
        {
            bool flag = false;
            if (Operator == "=")
            {
                if (ObjectPropertyValue == UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">")
            {
                if (ObjectPropertyValue > UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">=")
            {
                if (ObjectPropertyValue >= UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == "<")
            {
                if (ObjectPropertyValue < UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == "<=")
            {
                if (ObjectPropertyValue <= UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (!((Operator == "<>") | (Operator == "!=")))
            {
                throw new Exception("The operator '" + Operator + "' does not match the type int !");
            }
            if (ObjectPropertyValue != UserValue)
            {
                flag = true;
            }
            return flag;
        }

        private bool IsOk(double ObjectPropertyValue, string Operator, double UserValue)
        {
            bool flag = false;
            if (Operator == "=")
            {
                if (ObjectPropertyValue == UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">")
            {
                if (ObjectPropertyValue > UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">=")
            {
                if (ObjectPropertyValue >= UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == "<")
            {
                if (ObjectPropertyValue < UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator != "<=")
            {
                throw new Exception("The operator '" + Operator + "' does not match the type double !");
            }
            if (ObjectPropertyValue <= UserValue)
            {
                flag = true;
            }
            return flag;
        }

        private bool IsOk(Guid ObjectPropertyValue, string Operator, Guid UserValue)
        {
            bool flag = false;
            if (Operator != "=")
            {
                throw new Exception("The operator '" + Operator + "' does not match the type Guid !");
            }
            if (ObjectPropertyValue == UserValue)
            {
                flag = true;
            }
            return flag;
        }

        private bool IsOk(int ObjectPropertyValue, string Operator, int UserValue)
        {
            bool flag = false;
            if (Operator == "=")
            {
                if (ObjectPropertyValue == UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">")
            {
                if (ObjectPropertyValue > UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">=")
            {
                if (ObjectPropertyValue >= UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == "<")
            {
                if (ObjectPropertyValue < UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == "<=")
            {
                if (ObjectPropertyValue <= UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (!((Operator == "<>") | (Operator == "!=")))
            {
                throw new Exception("The operator '" + Operator + "' does not match the type int !");
            }
            if (ObjectPropertyValue != UserValue)
            {
                flag = true;
            }
            return flag;
        }

        private bool IsOk(long ObjectPropertyValue, string Operator, long UserValue)
        {
            bool flag = false;
            if (Operator == "=")
            {
                if (ObjectPropertyValue == UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">")
            {
                if (ObjectPropertyValue > UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == ">=")
            {
                if (ObjectPropertyValue >= UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator == "<")
            {
                if (ObjectPropertyValue < UserValue)
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator != "<=")
            {
                throw new Exception("The operator '" + Operator + "' does not match the type double !");
            }
            if (ObjectPropertyValue <= UserValue)
            {
                flag = true;
            }
            return flag;
        }

        private bool IsOk(object ObjectPropertyValue, string Operator, string UserValue)
        {
            if (ObjectPropertyValue == null)
            {
            }
            Type type = ObjectPropertyValue.GetType();
            object obj2 = this.CorrectUserValue(UserValue);
            if (type == typeof(string))
            {
                return this.IsOk((string) ObjectPropertyValue, Operator, (string) obj2);
            }
            if (type == typeof(int))
            {
                return this.IsOk((int) ObjectPropertyValue, Operator, Convert.ToInt32(obj2));
            }
            if (type == typeof(double))
            {
                return this.IsOk((double) ObjectPropertyValue, Operator, Convert.ToDouble(obj2));
            }
            if (type == typeof(decimal))
            {
                return this.IsOk((decimal) ObjectPropertyValue, Operator, Convert.ToDecimal(obj2));
            }
            if (type == typeof(DateTime))
            {
                return this.IsOk((DateTime) ObjectPropertyValue, Operator, Convert.ToDateTime(obj2));
            }
            if (type == typeof(bool))
            {
                return this.IsOk((bool) ObjectPropertyValue, Operator, Convert.ToBoolean(obj2));
            }
            if (type == typeof(Guid))
            {
                return this.IsOk((Guid) ObjectPropertyValue, Operator, new Guid(obj2.ToString()));
            }
            if (type == typeof(decimal))
            {
                return this.IsOk((decimal) ObjectPropertyValue, Operator, Convert.ToDecimal(obj2));
            }
            if (type == typeof(byte))
            {
                return this.IsOk((int) ((byte) ObjectPropertyValue), Operator, (int) Convert.ToByte(obj2));
            }
            if (type == typeof(long))
            {
                return this.IsOk((long) ObjectPropertyValue, Operator, Convert.ToInt64(obj2));
            }
            if (type != typeof(short))
            {
                throw new Exception("Filtering is not possible on the type " + type.ToString());
            }
            return this.IsOk((int) ((short) ObjectPropertyValue), Operator, (int) Convert.ToInt16(obj2));
        }

        private bool IsOk(string ObjectPropertyValue, string Operator, string UserValue)
        {
            bool flag = false;
            if (Operator == "=")
            {
                if (ObjectPropertyValue.TrimEnd(new char[0]) == UserValue.TrimEnd(new char[0]))
                {
                    flag = true;
                }
                return flag;
            }
            if ((Operator == "<>") | (Operator == "!="))
            {
                if (ObjectPropertyValue.TrimEnd(new char[0]) != UserValue.TrimEnd(new char[0]))
                {
                    flag = true;
                }
                return flag;
            }
            if (Operator.ToUpper() != "LIKE")
            {
                throw new Exception("The operator '" + Operator + "' does not match the type String !");
            }
            int num = UserValue.LastIndexOf("*");
            int length = UserValue.Length;
            string text = UserValue.Replace("*", "");
            if (ObjectPropertyValue.IndexOf(text) != -1)
            {
                flag = true;
            }
            return flag;
        }
    }
}

