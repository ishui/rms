namespace RmsOA.BFL
{
    using System;

    public class AssetModel
    {
        private int code;
        private string equiName;
        private string sortCode;

        public int Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        public string EquiName
        {
            get
            {
                return this.equiName;
            }
            set
            {
                this.equiName = value;
            }
        }

        public string SortCode
        {
            get
            {
                return this.sortCode;
            }
            set
            {
                this.sortCode = value;
            }
        }
    }
}

