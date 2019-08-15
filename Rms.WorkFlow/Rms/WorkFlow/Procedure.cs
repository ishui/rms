namespace Rms.WorkFlow
{
    using System;
    using System.Collections;

    [Serializable]
    public class Procedure
    {
        private int m_Activity;
        private string m_ApplicationInfoPath;
        private string m_ApplicationPath;
        private DateTime m_CreateDate;
        private string m_CreateUser;
        public Hashtable m_DataPackages = new Hashtable();
        private string m_Description;
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;
        private DateTime m_ModifyDate;
        private string m_ModifyUser;
        private string m_ProcedureCode;
        private string m_ProcedureName;
        private string m_ProcedureRemark;
        private string m_ProjectCode;
        public Hashtable m_Propertys = new Hashtable();
        private string m_Remark;
        public Hashtable m_Roles = new Hashtable();
        public Hashtable m_Routers = new Hashtable();
        private string m_SysType;
        public Hashtable m_Tasks = new Hashtable();
        private int m_Type;
        private string m_VersionDescription;
        private decimal m_VersionNumber;

        public void AddNewProperty(Property PropertyCase)
        {
            this.m_Propertys.Add(PropertyCase.WorkFlowProcedurePropertyCode, PropertyCase);
        }

        public void AddNewRole(Role RoleCase)
        {
            this.m_Roles.Add(RoleCase.WorkFlowRoleCode, RoleCase);
        }

        public void AddNewRouter(Router router)
        {
            this.m_Routers.Add(router.RouterCode, router);
        }

        public void AddNewTask(Task task)
        {
            this.m_Tasks.Add(task.TaskCode, task);
        }

        public Property GetProperty(string PropertyCode)
        {
            Property property = null;
            if (this.m_Propertys.Contains(PropertyCode))
            {
                property = (Property) this.m_Propertys[PropertyCode];
            }
            return property;
        }

        public IDictionaryEnumerator GetPropertyEnumerator()
        {
            return this.m_Propertys.GetEnumerator();
        }

        public Role GetRole(string RoleCode)
        {
            Role role = null;
            if (this.m_Roles.Contains(RoleCode))
            {
                role = (Role) this.m_Roles[RoleCode];
            }
            return role;
        }

        public IDictionaryEnumerator GetRoleEnumerator()
        {
            return this.m_Roles.GetEnumerator();
        }

        public Router GetRouter(string routerCode)
        {
            Router router = null;
            if (this.m_Routers.Contains(routerCode))
            {
                router = (Router) this.m_Routers[routerCode];
            }
            return router;
        }

        public IDictionaryEnumerator GetRouterEnumerator()
        {
            return this.m_Routers.GetEnumerator();
        }

        public Task GetTask(string taskCode)
        {
            Task task = null;
            if (this.m_Tasks.Contains(taskCode))
            {
                task = (Task) this.m_Tasks[taskCode];
            }
            return task;
        }

        public IDictionaryEnumerator GetTaskEnumerator()
        {
            return this.m_Tasks.GetEnumerator();
        }

        public void RemoveProperty(string PropertyCode)
        {
            this.m_Propertys.Remove(PropertyCode);
        }

        public void RemoveRole(string RoleCode)
        {
            this.m_Roles.Remove(RoleCode);
        }

        public void RemoveRouter(string routerCode, bool isMarkDelete)
        {
            if (isMarkDelete)
            {
                ((Router) this.m_Routers[routerCode]).IsMarkDelete = true;
            }
            else
            {
                this.m_Routers.Remove(routerCode);
            }
        }

        public void RemoveTask(string taskCode, bool isMarkDelete)
        {
            if (isMarkDelete)
            {
                ((Task) this.m_Tasks[taskCode]).IsMarkDelete = true;
            }
            else
            {
                this.m_Tasks.Remove(taskCode);
            }
            IDictionaryEnumerator enumerator = this.m_Routers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Router router = (Router) enumerator.Value;
                if (router.FromTaskCode == taskCode)
                {
                    router.FromTaskCode = "";
                }
                if (router.ToTaskCode == taskCode)
                {
                    router.ToTaskCode = "";
                }
            }
        }

        public int Activity
        {
            get
            {
                return this.m_Activity;
            }
            set
            {
                this.m_Activity = value;
            }
        }

        public string ApplicationInfoPath
        {
            get
            {
                return this.m_ApplicationInfoPath;
            }
            set
            {
                this.m_ApplicationInfoPath = value;
            }
        }

        public string ApplicationPath
        {
            get
            {
                return this.m_ApplicationPath;
            }
            set
            {
                this.m_ApplicationPath = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return this.m_CreateDate;
            }
            set
            {
                this.m_CreateDate = value;
            }
        }

        public string CreateUser
        {
            get
            {
                return this.m_CreateUser;
            }
            set
            {
                this.m_CreateUser = value;
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

        public DateTime ModifyDate
        {
            get
            {
                return this.m_ModifyDate;
            }
            set
            {
                this.m_ModifyDate = value;
            }
        }

        public string ModifyUser
        {
            get
            {
                return this.m_ModifyUser;
            }
            set
            {
                this.m_ModifyUser = value;
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

        public string ProcedureRemark
        {
            get
            {
                return this.m_ProcedureRemark;
            }
            set
            {
                this.m_ProcedureRemark = value;
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

        public string Remark
        {
            get
            {
                return this.m_Remark;
            }
            set
            {
                this.m_Remark = value;
            }
        }

        public string SysType
        {
            get
            {
                return this.m_SysType;
            }
            set
            {
                this.m_SysType = value;
            }
        }

        public int Type
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
            }
        }

        public string VersionDescription
        {
            get
            {
                return this.m_VersionDescription;
            }
            set
            {
                this.m_VersionDescription = value;
            }
        }

        public decimal VersionNumber
        {
            get
            {
                return this.m_VersionNumber;
            }
            set
            {
                this.m_VersionNumber = value;
            }
        }
    }
}

