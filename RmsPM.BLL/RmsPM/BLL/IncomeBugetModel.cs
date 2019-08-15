namespace RmsPM.BLL
{
    using System;

    public class IncomeBugetModel
    {
        private string id;
        private decimal money;
        private int month;
        private string projectCode;
        private int year;

        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public decimal Money
        {
            get
            {
                return this.money;
            }
            set
            {
                this.money = value;
            }
        }

        public int Month
        {
            get
            {
                return this.month;
            }
            set
            {
                this.month = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this.projectCode;
            }
            set
            {
                this.projectCode = value;
            }
        }

        public int Year
        {
            get
            {
                return this.year;
            }
            set
            {
                this.year = value;
            }
        }
    }
}

