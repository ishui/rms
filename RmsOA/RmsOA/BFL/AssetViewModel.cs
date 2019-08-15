namespace RmsOA.BFL
{
    using System;
    using RmsOA.MODEL;

    [Serializable]
    public class AssetViewModel : GK_OA_CapitalAssertAcountModel
    {
        private string deptName;
        private int index;
        private int noIndex;

        public string DeptName
        {
            get
            {
                return this.deptName;
            }
            set
            {
                this.deptName = value;
            }
        }

        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }

        public int NoIndex
        {
            get
            {
                return this.noIndex;
            }
            set
            {
                this.noIndex = value;
            }
        }
    }
}

