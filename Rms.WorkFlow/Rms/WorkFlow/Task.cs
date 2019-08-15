namespace Rms.WorkFlow
{
    using System;
    using System.Collections;

    public class Task
    {
        private int m_CanEdit;
        private int m_CanManual;
        private int m_Copy;
        private string m_Description;
        private int m_HasOpinion;
        private int m_IsFinish;
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;
        private int m_IsOrderly;
        private string m_ModuleState;
        private string m_OpinionType;
        private string m_ProcedureCode;
        private int m_SortID;
        public Hashtable m_TaskActors = new Hashtable();
        private string m_TaskActorType;
        private string m_TaskCode;
        private string m_TaskID;
        private string m_TaskMeetType;
        private string m_TaskName;
        private string m_TaskProperty;
        private string m_TaskRole;
        private string m_TaskTitle;
        private int m_TaskType;
        private string m_WayOfSelectPerson;

        public void AddNewTaskActor(TaskActor taskActor)
        {
            this.m_TaskActors.Add(taskActor.TaskActorCode, taskActor);
        }

        public TaskActor GetTaskActor(string taskActorCode)
        {
            return (TaskActor) this.m_TaskActors[taskActorCode];
        }

        public IDictionaryEnumerator GetTaskActorEnumerator()
        {
            return this.m_TaskActors.GetEnumerator();
        }

        public void RemoveTaskActor(string taskActorCode, bool isMarkDelete)
        {
            if (isMarkDelete)
            {
                ((TaskActor) this.m_TaskActors[taskActorCode]).IsMarkDelete = true;
            }
            else
            {
                this.m_TaskActors.Remove(taskActorCode);
            }
        }

        public int CanEdit
        {
            get
            {
                return this.m_CanEdit;
            }
            set
            {
                this.m_CanEdit = value;
            }
        }

        public int CanManual
        {
            get
            {
                return this.m_CanManual;
            }
            set
            {
                this.m_CanManual = value;
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

        public int HasOpinion
        {
            get
            {
                return this.m_HasOpinion;
            }
            set
            {
                this.m_HasOpinion = value;
            }
        }

        public int IsFinish
        {
            get
            {
                return this.m_IsFinish;
            }
            set
            {
                this.m_IsFinish = value;
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

        public int IsOrderly
        {
            get
            {
                return this.m_IsOrderly;
            }
            set
            {
                this.m_IsOrderly = value;
            }
        }

        public string ModuleState
        {
            get
            {
                return this.m_ModuleState;
            }
            set
            {
                this.m_ModuleState = value;
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

        public string TaskMeetType
        {
            get
            {
                return this.m_TaskMeetType;
            }
            set
            {
                this.m_TaskMeetType = value;
            }
        }

        public string TaskName
        {
            get
            {
                return this.m_TaskName;
            }
            set
            {
                this.m_TaskName = value;
            }
        }

        public string TaskProperty
        {
            get
            {
                return this.m_TaskProperty;
            }
            set
            {
                this.m_TaskProperty = value;
            }
        }

        public string TaskRole
        {
            get
            {
                return this.m_TaskRole;
            }
            set
            {
                this.m_TaskRole = value;
            }
        }

        public string TaskTitle
        {
            get
            {
                return this.m_TaskTitle;
            }
            set
            {
                this.m_TaskTitle = value;
            }
        }

        public int TaskType
        {
            get
            {
                return this.m_TaskType;
            }
            set
            {
                this.m_TaskType = value;
            }
        }

        public string WayOfSelectPerson
        {
            get
            {
                return this.m_WayOfSelectPerson;
            }
            set
            {
                this.m_WayOfSelectPerson = value;
            }
        }
    }
}

