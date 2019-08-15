namespace RmsOA.MODEL
{
    using System;
    using System.Collections.Generic;

    public class DeptSupplyModel
    {
        private string deptCode;
        private string deptName;
        private List<string> supplyName;

        public string DeptCode
        {
            get
            {
                return this.deptCode;
            }
            set
            {
                this.deptCode = value;
            }
        }

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

        public List<string> SupplyName
        {
            get
            {
                return this.supplyName;
            }
            set
            {
                this.supplyName = value;
            }
        }
    }
}

