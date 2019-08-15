namespace Rms.WorkFlow
{
    using System;

    public class DataPackage
    {
        private string m_ActCode;
        private string m_CaseCode;
        private string m_DataKey;
        private string m_DataPackageCode;
        private string m_DataValue;
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;
        private string m_ProcedureCode;

        public string ActCode
        {
            get
            {
                return this.m_ActCode;
            }
            set
            {
                this.m_ActCode = value;
            }
        }

        public string CaseCode
        {
            get
            {
                return this.m_CaseCode;
            }
            set
            {
                this.m_CaseCode = value;
            }
        }

        public string DataKey
        {
            get
            {
                return this.m_DataKey;
            }
            set
            {
                this.m_DataKey = value;
            }
        }

        public string DataPackageCode
        {
            get
            {
                return this.m_DataPackageCode;
            }
            set
            {
                this.m_DataPackageCode = value;
            }
        }

        public string DataValue
        {
            get
            {
                return this.m_DataValue;
            }
            set
            {
                this.m_DataValue = value;
            }
        }

        public bool IsMarkDelete
        {
            get
            {
                return this.m_IsMarkDelete;
            }
            set
            {
                this.m_IsMarkDelete = value;
            }
        }

        public bool IsNew
        {
            get
            {
                return this.m_IsNew;
            }
            set
            {
                this.m_IsNew = value;
            }
        }

        public string ProcedureCode
        {
            get
            {
                return this.m_ProcedureCode;
            }
            set
            {
                this.m_ProcedureCode = value;
            }
        }
    }
}

