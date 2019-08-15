namespace Rms.WorkFlow
{
    using System;

    public class Condition
    {
        private string m_ConditionCode;
        private int m_ConditionType;
        private string m_Description;
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;
        private string m_ProcedureCode;
        private string m_RouterCode;

        public string ConditionCode
        {
            get
            {
                return this.m_ConditionCode;
            }
            set
            {
                this.m_ConditionCode = value;
            }
        }

        public int ConditionType
        {
            get
            {
                return this.m_ConditionType;
            }
            set
            {
                this.m_ConditionType = value;
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
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

        public string RouterCode
        {
            get
            {
                return this.m_RouterCode;
            }
            set
            {
                this.m_RouterCode = value;
            }
        }
    }
}

