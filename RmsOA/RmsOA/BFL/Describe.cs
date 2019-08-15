namespace RmsOA.BFL
{
    using System;

    public static class Describe
    {
        public static string GetFiled(object o)
        {
            EnumFiledDescribeAttribute attribute = (EnumFiledDescribeAttribute) o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(EnumFiledDescribeAttribute), false)[0];
            return attribute.Mean;
        }
    }
}

