namespace RmsOA.MODEL
{
    using System;

    public class GK_OA_CarModel
    {
        private DateTime _Buy_Date;
        private int _Car_Code;
        private string _Car_Id;
        private string _Car_Type;
        private string _Chejia_Id;
        private string _Fadongji_Id;
        private string _Index_num;

        public DateTime Buy_Date
        {
            get
            {
                return this._Buy_Date;
            }
            set
            {
                this._Buy_Date = value;
            }
        }

        public int Car_Code
        {
            get
            {
                return this._Car_Code;
            }
            set
            {
                this._Car_Code = value;
            }
        }

        public string Car_Id
        {
            get
            {
                return this._Car_Id;
            }
            set
            {
                this._Car_Id = value;
            }
        }

        public string Car_Type
        {
            get
            {
                return this._Car_Type;
            }
            set
            {
                this._Car_Type = value;
            }
        }

        public string Chejia_Id
        {
            get
            {
                return this._Chejia_Id;
            }
            set
            {
                this._Chejia_Id = value;
            }
        }

        public string Fadongji_Id
        {
            get
            {
                return this._Fadongji_Id;
            }
            set
            {
                this._Fadongji_Id = value;
            }
        }

        public string Index_num
        {
            get
            {
                return this._Index_num;
            }
            set
            {
                this._Index_num = value;
            }
        }
    }
}

