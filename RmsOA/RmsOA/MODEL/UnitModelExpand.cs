namespace RmsOA.MODEL
{
    using System;

    public class UnitModelExpand
    {
        private string expandName;
        private string unitCode;
        private string unitName;

        public string ExpandName
        {
            get
            {
                return this.expandName;
            }
            set
            {
                this.expandName = value;
            }
        }

        public string UnitCode
        {
            get
            {
                return this.unitCode;
            }
            set
            {
                this.unitCode = value;
            }
        }

        public string UnitName
        {
            get
            {
                return this.unitName;
            }
            set
            {
                this.unitName = value;
            }
        }
    }
}

