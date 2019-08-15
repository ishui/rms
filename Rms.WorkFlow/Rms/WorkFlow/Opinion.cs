namespace Rms.WorkFlow
{
    using System;

    public class Opinion
    {
        private string m_ApplicationCode;
        private string m_CaseCode;
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;
        private string m_OpinionCode;
        private string m_OpinionDate;
        private string m_OpinionText;
        private string m_OpinionType;
        private string m_ProcedureCode;
        private string m_TaskActorID;
        private string m_TaskActorName;
        private string m_TaskCode;
        private string m_TaskID;
        private string m_UserCode;

        public string ApplicationCode
        {
            get
            {
                return this.m_ApplicationCode;
            }
            set
            {
                this.m_ApplicationCode = value;
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

        public string OpinionCode
        {
            get
            {
                return this.m_OpinionCode;
            }
            set
            {
                this.m_OpinionCode = value;
            }
        }

        public string OpinionDate
        {
            get
            {
                return this.m_OpinionDate;
            }
            set
            {
                this.m_OpinionDate = value;
            }
        }

        public string OpinionText
        {
            get
            {
                return this.m_OpinionText;
            }
            set
            {
                this.m_OpinionText = value;
            }
        }

        public string OpinionType
        {
            get
            {
                return this.m_OpinionType;
            }
            set
            {
                this.m_OpinionType = value;
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

        public string TaskActorID
        {
            get
            {
                return this.m_TaskActorID;
            }
            set
            {
                this.m_TaskActorID = value;
            }
        }

        public string TaskActorName
        {
            get
            {
                return this.m_TaskActorName;
            }
            set
            {
                this.m_TaskActorName = value;
            }
        }

        public string TaskCode
        {
            get
            {
                return this.m_TaskCode;
            }
            set
            {
                this.m_TaskCode = value;
            }
        }

        public string TaskID
        {
            get
            {
                return this.m_TaskID;
            }
            set
            {
                this.m_TaskID = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this.m_UserCode;
            }
            set
            {
                this.m_UserCode = value;
            }
        }
    }
}

