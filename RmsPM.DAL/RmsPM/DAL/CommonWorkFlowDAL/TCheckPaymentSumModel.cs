﻿namespace RmsPM.DAL.CommonWorkFlowDAL
{
    using System;

    public class TCheckPaymentSumModel
    {
        private string _Auditing;
        private int _Code;
        private string _SendUnit;
        private DateTime _SumDate;

        public string Auditing
        {
            get
            {
                return this._Auditing;
            }
            set
            {
                this._Auditing = value;
            }
        }

        public int Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                this._Code = value;
            }
        }

        public string SendUnit
        {
            get
            {
                return this._SendUnit;
            }
            set
            {
                this._SendUnit = value;
            }
        }

        public DateTime SumDate
        {
            get
            {
                return this._SumDate;
            }
            set
            {
                this._SumDate = value;
            }
        }
    }
}

