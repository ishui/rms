namespace Rms.WorkFlow
{
    using System;

    public class TaskActor
    {
        private string m_ActorCode;
        private string m_ActorModuleState;
        private string m_ActorNeed;
        private string m_ActorProperty;
        private int m_ActorType;
        private int m_IOrder;
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;
        private string m_OpinionType;
        private string m_ProcedureCode;
        private string m_TaskActorCode;
        private string m_TaskActorID;
        private string m_TaskActorName;
        private string m_TaskActorType;
        private string m_TaskCode;

        public string ActorCode
        {
            get
            {
                return this.m_ActorCode;
            }
            set
            {
                this.m_ActorCode = value;
            }
        }

        public string ActorModuleState
        {
            get
            {
                return this.m_ActorModuleState;
            }
            set
            {
                this.m_ActorModuleState = value;
            }
        }

        public string ActorNeed
        {
            get
            {
                return this.m_ActorNeed;
            }
            set
            {
                this.m_ActorNeed = value;
            }
        }

        public string ActorProperty
        {
            get
            {
                return this.m_ActorProperty;
            }
            set
            {
                this.m_ActorProperty = value;
            }
        }

        public int ActorType
        {
            get
            {
                return this.m_ActorType;
            }
            set
            {
                this.m_ActorType = value;
            }
        }

        public int IOrder
        {
            get
            {
                return this.m_IOrder;
            }
            set
            {
                this.m_IOrder = value;
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

        public string TaskActorCode
        {
            get
            {
                return this.m_TaskActorCode;
            }
            set
            {
                this.m_TaskActorCode = value;
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

        public string TaskActorType
        {
            get
            {
                return this.m_TaskActorType;
            }
            set
            {
                this.m_TaskActorType = value;
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
    }
}

