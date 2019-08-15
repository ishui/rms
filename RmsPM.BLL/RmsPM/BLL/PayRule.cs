namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;

    public class PayRule
    {
        private string _ApplicationCode = "";
        private DataTable _DetailTB = null;
        private EntityData _Entity = null;
        private DataRow _MessageDR = null;
        private string _PayType = "";

        private void FineType()
        {
            this.GetNewRow();
            string text = this._PayType;
            if ((text != null) && (text == "Vise"))
            {
                ViseSystem.GetPayDetail(this._DetailTB, this._ApplicationCode);
            }
            this.SetEntity();
        }

        private void GetNewRow()
        {
            this._Entity = new EntityData("Standard_Payment");
            this._MessageDR = this._Entity.Tables["Payment"].NewRow();
            this._DetailTB = this._Entity.Tables["PaymentItem"];
        }

        private void SetEntity()
        {
            this._Entity.Tables["Payment"].Rows.Add(this._MessageDR);
        }

        public string ApplicationCode
        {
            get
            {
                return this._ApplicationCode;
            }
            set
            {
                this._ApplicationCode = value;
            }
        }

        public DataTable DetailTB
        {
            get
            {
                if (this._DetailTB == null)
                {
                    this.FineType();
                }
                return this._DetailTB;
            }
            set
            {
                this._DetailTB = value;
            }
        }

        public EntityData Entity
        {
            get
            {
                if (this._Entity == null)
                {
                    this.FineType();
                }
                return this._Entity;
            }
            set
            {
                this._Entity = value;
            }
        }

        public DataRow MessageDR
        {
            get
            {
                if (this._MessageDR == null)
                {
                    this.FineType();
                }
                return this._MessageDR;
            }
            set
            {
                this._MessageDR = value;
            }
        }

        public string PayType
        {
            get
            {
                return this._PayType;
            }
            set
            {
                this._PayType = value;
            }
        }
    }
}

