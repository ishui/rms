namespace RmsOA.MODEL
{
    using System;

    public class UnitModel
    {
        private string unitCode;
        private string unitName;

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

