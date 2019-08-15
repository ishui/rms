namespace RmsOA.BFL
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public class EnumFiledDescribeAttribute : Attribute
    {
        private string mean;

        public EnumFiledDescribeAttribute(string mean)
        {
            this.mean = mean;
        }

        public string Mean
        {
            get
            {
                return this.mean;
            }
            set
            {
                this.mean = value;
            }
        }
    }
}

