namespace Rms.WorkFlow
{
    using System;

    public class ActUser
    {
        private string m_ActCode;
        private string m_ActUserCode;
        private string m_CaseCode;
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;
        private string m_UserCode;

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

        public string ActUserCode
        {
            get
            {
                return this.m_ActUserCode;
            }
            set
            {
                this.m_ActUserCode = value;
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

