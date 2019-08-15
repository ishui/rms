namespace Rms.WorkFlow
{
    using System;
    using System.Collections;

    public class Act
    {
        private string m_ActCode;
        private int m_ActType;
        private string m_ActUnitCode;
        private string m_ActUserCode;
        private Hashtable m_ActUsers = new Hashtable();
        private string m_ApplicationCode;
        private string m_ApplicationSubject;
        private string m_CaseCode;
        private int m_Copy;
        private string m_CopyFromActCode;
        private string m_FinishDate = "";
        private string m_FromDate = "";
        private string m_FromTaskCode;
        private string m_FromTaskName;
        private string m_FromUnitCode;
        private string m_FromUserCode;
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;
        private int m_IsSleep;
        private string m_PlanDate = "";
        private string m_ProcedureCode;
        private string m_ProcedureName;
        private string m_ProjectCode;
        private string m_RouterCode;
        private string m_SignDate = "";
        private ActStatus m_Status;
        private string m_TaskActorID;
        private string m_TaskActorName;
        private string m_ToTaskCode;
        private string m_ToTaskName;

        public void AddNewActUser(ActUser actUser)
        {
            this.m_ActUsers.Add(actUser.ActUserCode, actUser);
        }

        public ActUser GetActUser(string ActUserCode)
        {
            return (ActUser) this.m_ActUsers[ActUserCode];
        }

        public IDictionaryEnumerator GetActUserEnumerator()
        {
            return this.m_ActUsers.GetEnumerator();
        }

        public void RemoveActUser(string actUserCode, bool isMarkDelete)
        {
            if (isMarkDelete)
            {
                ((ActUser) this.m_ActUsers[this.ActUserCode]).IsMarkDelete = true;
            }
            else
            {
                this.m_ActUsers.Remove(this.ActUserCode);
            }
        }

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

        public int ActType
        {
            get
            {
                return this.m_ActType;
            }
            set
            {
                this.m_ActType = value;
            }
        }

        public string ActUnitCode
        {
            get
            {
                return this.m_ActUnitCode;
            }
            set
            {
                this.m_ActUnitCode = value;
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

        public string ApplicationSubject
        {
            get
            {
                return this.m_ApplicationSubject;
            }
            set
            {
                this.m_ApplicationSubject = value;
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

        public int Copy
        {
            get
            {
                return this.m_Copy;
            }
            set
            {
                this.m_Copy = value;
            }
        }

        public string CopyFromActCode
        {
            get
            {
                return this.m_CopyFromActCode;
            }
            set
            {
                this.m_CopyFromActCode = value;
            }
        }

        public string FinishDate
        {
            get
            {
                return this.m_FinishDate;
            }
            set
            {
                this.m_FinishDate = value;
            }
        }

        public string FromDate
        {
            get
            {
                return this.m_FromDate;
            }
            set
            {
                this.m_FromDate = value;
            }
        }

        public string FromTaskCode
        {
            get
            {
                return this.m_FromTaskCode;
            }
            set
            {
                this.m_FromTaskCode = value;
            }
        }

        public string FromTaskName
        {
            get
            {
                return this.m_FromTaskName;
            }
            set
            {
                this.m_FromTaskName = value;
            }
        }

        public string FromUnitCode
        {
            get
            {
                return this.m_FromUnitCode;
            }
            set
            {
                this.m_FromUnitCode = value;
            }
        }

        public string FromUserCode
        {
            get
            {
                return this.m_FromUserCode;
            }
            set
            {
                this.m_FromUserCode = value;
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

        public int IsSleep
        {
            get
            {
                return this.m_IsSleep;
            }
            set
            {
                this.m_IsSleep = value;
            }
        }

        public string PlanDate
        {
            get
            {
                return this.m_PlanDate;
            }
            set
            {
                this.m_PlanDate = value;
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

        public string ProcedureName
        {
            get
            {
                return this.m_ProcedureName;
            }
            set
            {
                this.m_ProcedureName = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this.m_ProjectCode;
            }
            set
            {
                this.m_ProjectCode = value;
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

        public string SignDate
        {
            get
            {
                return this.m_SignDate;
            }
            set
            {
                this.m_SignDate = value;
            }
        }

        public ActStatus Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
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

        public string ToTaskCode
        {
            get
            {
                return this.m_ToTaskCode;
            }
            set
            {
                this.m_ToTaskCode = value;
            }
        }

        public string ToTaskName
        {
            get
            {
                return this.m_ToTaskName;
            }
            set
            {
                this.m_ToTaskName = value;
            }
        }
    }
}

