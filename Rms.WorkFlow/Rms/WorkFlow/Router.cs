namespace Rms.WorkFlow
{
    using System;
    using System.Collections;

    public class Router
    {
        public Hashtable m_Conditions = new Hashtable();
        private string m_Description;
        private string m_FromTaskCode;
        private string m_FromTaskName;
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;
        private string m_ProcedureCode;
        private string m_RouterCode;
        private string m_SoftID;
        private int m_SortID;
        private int m_ToHandle;
        private string m_ToTaskCode;
        private string m_ToTaskName;

        public void AddNewCondition(Condition condition)
        {
            this.m_Conditions.Add(condition.ConditionCode, condition);
        }

        public Condition GetCondition(string conditionCode)
        {
            return (Condition) this.m_Conditions[conditionCode];
        }

        public IDictionaryEnumerator GetConditionEnumerator()
        {
            return this.m_Conditions.GetEnumerator();
        }

        public void RemoveCondition(string conditionCode, bool isMarkDelete)
        {
            if (isMarkDelete)
            {
                ((Condition) this.m_Conditions[conditionCode]).IsMarkDelete = true;
            }
            else
            {
                this.m_Conditions.Remove(conditionCode);
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

        public string SoftID
        {
            get
            {
                return this.m_SoftID;
            }
            set
            {
                this.m_SoftID = value;
            }
        }

        public int SortID
        {
            get
            {
                return this.m_SortID;
            }
            set
            {
                this.m_SortID = value;
            }
        }

        public int ToHandle
        {
            get
            {
                return this.m_ToHandle;
            }
            set
            {
                this.m_ToHandle = value;
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

