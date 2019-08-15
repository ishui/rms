namespace Rms.WorkFlow
{
    using System;
    using System.Collections;
    using System.Data;

    public sealed class DefinitionManager
    {
        private static Hashtable m_ProcedureDefinitions = new Hashtable();

        private DefinitionManager()
        {
        }

        public static bool CheckProcedureDinition(Procedure procedure, ref string Message)
        {
            bool flag2;
            try
            {
                bool flag = true;
                int num = 0;
                int num2 = 0;
                IDictionaryEnumerator taskEnumerator = procedure.GetTaskEnumerator();
                while (taskEnumerator.MoveNext())
                {
                    Task task = (Task) taskEnumerator.Value;
                    if (task.TaskID == "Begin")
                    {
                        num++;
                    }
                    if (task.TaskID == "End")
                    {
                        num2++;
                    }
                }
                if (num != 1)
                {
                    flag = false;
                    Message = Message + "流程有且只能有一个开始点。 ";
                }
                if (num2 != 1)
                {
                    flag = false;
                    Message = Message + "流程有且只能有一个结束点。 ";
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return flag2;
        }

        private static void DeleteAllTableRow(DataTable dt)
        {
            dt.Rows.Clear();
        }

        private static void FillCondition(Condition condition, DataRow dr)
        {
            if (!dr.IsNull("ConditionCode"))
            {
                condition.ConditionCode = (string) dr["ConditionCode"];
            }
            if (!dr.IsNull("RouterCode"))
            {
                condition.RouterCode = (string) dr["RouterCode"];
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                condition.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("ConditionType"))
            {
                condition.ConditionType = (int) dr["ConditionType"];
            }
            if (!dr.IsNull("Description"))
            {
                condition.Description = (string) dr["Description"];
            }
        }

        private static void FillProcedure(Procedure procedure, DataRow dr)
        {
            if (!dr.IsNull("ProcedureCode"))
            {
                procedure.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("ProcedureName"))
            {
                procedure.ProcedureName = (string) dr["ProcedureName"];
            }
            if (!dr.IsNull("Description"))
            {
                procedure.Description = (string) dr["Description"];
            }
            if (!dr.IsNull("ApplicationPath"))
            {
                procedure.ApplicationPath = (string) dr["ApplicationPath"];
            }
            if (!dr.IsNull("ApplicationInfoPath"))
            {
                procedure.ApplicationInfoPath = (string) dr["ApplicationInfoPath"];
            }
            if (!dr.IsNull("Type"))
            {
                procedure.Type = (int) dr["Type"];
            }
            if (!dr.IsNull("SysType"))
            {
                procedure.SysType = (string) dr["SysType"];
            }
            if (!dr.IsNull("Remark"))
            {
                procedure.Remark = (string) dr["Remark"];
            }
            if (!dr.IsNull("VersionNumber"))
            {
                procedure.VersionNumber = (decimal) dr["VersionNumber"];
            }
            if (!dr.IsNull("ProjectCode"))
            {
                procedure.ProjectCode = (string) dr["ProjectCode"];
            }
            if (!dr.IsNull("CreateUser"))
            {
                procedure.CreateUser = (string) dr["CreateUser"];
            }
            if (!dr.IsNull("CreateDate"))
            {
                procedure.CreateDate = (DateTime) dr["CreateDate"];
            }
            if (!dr.IsNull("ModifyUser"))
            {
                procedure.ModifyUser = (string) dr["ModifyUser"];
            }
            if (!dr.IsNull("ModifyDate"))
            {
                procedure.ModifyDate = (DateTime) dr["ModifyDate"];
            }
            if (!dr.IsNull("Activity"))
            {
                procedure.Activity = (int) dr["Activity"];
            }
            if (!dr.IsNull("VersionDescription"))
            {
                procedure.VersionDescription = (string) dr["VersionDescription"];
            }
            if (!dr.IsNull("ProcedureRemark"))
            {
                procedure.ProcedureRemark = (string) dr["ProcedureRemark"];
            }
        }

        private static void FillProperty(Property PropertyCase, DataRow dr)
        {
            if (!dr.IsNull("WorkFlowProcedurePropertyCode"))
            {
                PropertyCase.WorkFlowProcedurePropertyCode = (string) dr["WorkFlowProcedurePropertyCode"];
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                PropertyCase.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("ProcedurePropertyName"))
            {
                PropertyCase.ProcedurePropertyName = (string) dr["ProcedurePropertyName"];
            }
            if (!dr.IsNull("ProcedurePropertyType"))
            {
                PropertyCase.ProcedurePropertyType = (string) dr["ProcedurePropertyType"];
            }
            if (!dr.IsNull("Remak"))
            {
                PropertyCase.Remak = (string) dr["Remak"];
            }
        }

        private static void FillRole(Role RoleCase, DataRow dr)
        {
            if (!dr.IsNull("WorkFlowRoleCode"))
            {
                RoleCase.WorkFlowRoleCode = (string) dr["WorkFlowRoleCode"];
            }
            if (!dr.IsNull("RoleName"))
            {
                RoleCase.RoleName = (string) dr["RoleName"];
            }
            if (!dr.IsNull("RoleType"))
            {
                RoleCase.RoleType = (string) dr["RoleType"];
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                RoleCase.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("IsAllUser"))
            {
                RoleCase.IsAllUser = (string) dr["IsAllUser"];
            }
            if (!dr.IsNull("Remak"))
            {
                RoleCase.Remak = (string) dr["Remak"];
            }
        }

        private static void FillRoleComprise(RoleComprise RoleCompriseCase, DataRow dr)
        {
            if (!dr.IsNull("WorkFlowRoleCompriseCode"))
            {
                RoleCompriseCase.WorkFlowRoleCompriseCode = (string) dr["WorkFlowRoleCompriseCode"];
            }
            if (!dr.IsNull("WorkFlowRoleCode"))
            {
                RoleCompriseCase.RoleCode = (string) dr["WorkFlowRoleCode"];
            }
            if (!dr.IsNull("RoleComprise"))
            {
                RoleCompriseCase.RoleCompriseItem = (string) dr["RoleComprise"];
            }
            if (!dr.IsNull("RoleType"))
            {
                RoleCompriseCase.RoleType = new RoleTypeManage().GetStringRoleTypeItem((string) dr["RoleType"]);
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                RoleCompriseCase.ProcedureCode = (string) dr["ProcedureCode"];
            }
        }

        private static void FillRouter(Router router, DataRow dr)
        {
            if (!dr.IsNull("RouterCode"))
            {
                router.RouterCode = (string) dr["RouterCode"];
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                router.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("FromTaskCode"))
            {
                router.FromTaskCode = (string) dr["FromTaskCode"];
            }
            if (!dr.IsNull("ToTaskCode"))
            {
                router.ToTaskCode = (string) dr["ToTaskCode"];
            }
            if (!dr.IsNull("Description"))
            {
                router.Description = (string) dr["Description"];
            }
            if (!dr.IsNull("ToHandle"))
            {
                router.ToHandle = (int) dr["ToHandle"];
            }
            if (!dr.IsNull("SoftID"))
            {
                router.SoftID = (string) dr["SoftID"];
            }
            if (!dr.IsNull("SortID"))
            {
                router.SortID = (int) dr["SortID"];
            }
        }

        private static void FillTask(Task task, DataRow dr)
        {
            if (!dr.IsNull("TaskCode"))
            {
                task.TaskCode = (string) dr["TaskCode"];
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                task.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("TaskID"))
            {
                task.TaskID = (string) dr["TaskID"];
            }
            if (!dr.IsNull("TaskName"))
            {
                task.TaskName = (string) dr["TaskName"];
            }
            if (!dr.IsNull("Description"))
            {
                task.Description = (string) dr["Description"];
            }
            if (!dr.IsNull("TaskType"))
            {
                task.TaskType = (int) dr["TaskType"];
            }
            if (!dr.IsNull("TaskActorType"))
            {
                task.TaskActorType = (string) dr["TaskActorType"];
            }
            if (!dr.IsNull("WayOfSelectPerson"))
            {
                task.WayOfSelectPerson = (string) dr["WayOfSelectPerson"];
            }
            if (!dr.IsNull("IsOrderly"))
            {
                task.IsOrderly = (int) dr["IsOrderly"];
            }
            if (!dr.IsNull("CanManual"))
            {
                task.CanManual = (int) dr["CanManual"];
            }
            if (!dr.IsNull("CanEdit"))
            {
                task.CanEdit = (int) dr["CanEdit"];
            }
            if (!dr.IsNull("IsFinish"))
            {
                task.IsFinish = (int) dr["IsFinish"];
            }
            if (!dr.IsNull("HasOpinion"))
            {
                task.HasOpinion = (int) dr["HasOpinion"];
            }
            if (!dr.IsNull("SortID"))
            {
                task.SortID = (int) dr["SortID"];
            }
            if (!dr.IsNull("Copy"))
            {
                task.Copy = (int) dr["Copy"];
            }
            if (!dr.IsNull("ModuleState"))
            {
                task.ModuleState = (string) dr["ModuleState"];
            }
            if (!dr.IsNull("TaskProperty"))
            {
                task.TaskProperty = (string) dr["TaskProperty"];
            }
            if (!dr.IsNull("TaskTitle"))
            {
                task.TaskTitle = (string) dr["TaskTitle"];
            }
            if (!dr.IsNull("TaskRole"))
            {
                task.TaskRole = (string) dr["TaskRole"];
            }
            if (!dr.IsNull("TaskMeetType"))
            {
                task.TaskMeetType = (string) dr["TaskMeetType"];
            }
            if (!dr.IsNull("OpinionType"))
            {
                task.OpinionType = (string) dr["OpinionType"];
            }
        }

        private static void FillTaskActor(TaskActor taskActor, DataRow dr)
        {
            if (!dr.IsNull("TaskActorCode"))
            {
                taskActor.TaskActorCode = (string) dr["TaskActorCode"];
            }
            if (!dr.IsNull("TaskCode"))
            {
                taskActor.TaskCode = (string) dr["TaskCode"];
            }
            if (!dr.IsNull("ProcedureCode"))
            {
                taskActor.ProcedureCode = (string) dr["ProcedureCode"];
            }
            if (!dr.IsNull("ActorCode"))
            {
                taskActor.ActorCode = (string) dr["ActorCode"];
            }
            if (!dr.IsNull("ActorType"))
            {
                taskActor.ActorType = (int) dr["ActorType"];
            }
            if (!dr.IsNull("IOrder"))
            {
                taskActor.IOrder = (int) dr["IOrder"];
            }
            if (!dr.IsNull("TaskActorName"))
            {
                taskActor.TaskActorName = (string) dr["TaskActorName"];
            }
            if (!dr.IsNull("TaskActorID"))
            {
                taskActor.TaskActorID = (string) dr["TaskActorID"];
            }
            if (!dr.IsNull("ActorProperty"))
            {
                taskActor.ActorProperty = (string) dr["ActorProperty"];
            }
            if (!dr.IsNull("ActorNeed"))
            {
                taskActor.ActorNeed = (string) dr["ActorNeed"];
            }
            if (!dr.IsNull("ActorModuleState"))
            {
                taskActor.ActorModuleState = (string) dr["ActorModuleState"];
            }
            if (!dr.IsNull("TaskActorType"))
            {
                taskActor.TaskActorType = (string) dr["TaskActorType"];
            }
            if (!dr.IsNull("OpinionType"))
            {
                taskActor.OpinionType = (string) dr["OpinionType"];
            }
        }

        public static string GerProcedureName(string procedureCode)
        {
            string procedureName;
            try
            {
                procedureName = GetProcedureDifinition(procedureCode, true).ProcedureName;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return procedureName;
        }

        public static Task GetBeginTask(Procedure procedure)
        {
            Task task3;
            try
            {
                Task task = null;
                if (procedure == null)
                {
                    return task;
                }
                string taskCode = "";
                IDictionaryEnumerator taskEnumerator = procedure.GetTaskEnumerator();
                while (taskEnumerator.MoveNext())
                {
                    Task task2 = (Task) taskEnumerator.Value;
                    if (task2.TaskType == 1)
                    {
                        taskCode = task2.TaskCode;
                        break;
                    }
                }
                if (taskCode == "")
                {
                    return task;
                }
                task3 = procedure.GetTask(taskCode);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return task3;
        }

        public static Task GetEndTask(Procedure procedure)
        {
            Task task3;
            try
            {
                Task task = null;
                IDictionaryEnumerator taskEnumerator = procedure.GetTaskEnumerator();
                while (taskEnumerator.MoveNext())
                {
                    Task task2 = (Task) taskEnumerator.Value;
                    if (task2.TaskType == 2)
                    {
                        task = task2;
                        break;
                    }
                }
                task3 = task;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return task3;
        }

        public static Task GetFirstTask(Procedure procedure)
        {
            Task task;
            try
            {
                Task beginTask = GetBeginTask(procedure);
                if (beginTask == null)
                {
                    return null;
                }
                string taskCode = "";
                IDictionaryEnumerator routerEnumerator = procedure.GetRouterEnumerator();
                while (routerEnumerator.MoveNext())
                {
                    Router router = (Router) routerEnumerator.Value;
                    if (router.FromTaskCode == beginTask.TaskCode)
                    {
                        taskCode = router.ToTaskCode;
                        break;
                    }
                }
                if (taskCode == "")
                {
                    return null;
                }
                task = procedure.GetTask(taskCode);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return task;
        }

        public static Procedure GetProcedureDifinition(string procedureCode, bool isStock)
        {
            Procedure procedure2;
            try
            {
                if (isStock)
                {
                    if (!m_ProcedureDefinitions.Contains(procedureCode))
                    {
                        Procedure procedure = LoadProcedureDefinition(procedureCode);
                        m_ProcedureDefinitions.Add(procedureCode, procedure);
                    }
                    return (Procedure) m_ProcedureDefinitions[procedureCode];
                }
                procedure2 = LoadProcedureDefinition(procedureCode);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return procedure2;
        }

        public static string GetTaskName(Procedure procedure, string taskCode)
        {
            string taskName;
            try
            {
                if (taskCode == "")
                {
                    return "";
                }
                taskName = procedure.GetTask(taskCode).TaskName;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return taskName;
        }

        public static ArrayList GetTaskRouter(Procedure procedure, string taskCode)
        {
            ArrayList list2;
            try
            {
                ArrayList list = new ArrayList();
                IDictionaryEnumerator routerEnumerator = procedure.GetRouterEnumerator();
                while (routerEnumerator.MoveNext())
                {
                    Router router = (Router) routerEnumerator.Value;
                    if (router.FromTaskCode == taskCode)
                    {
                        list.Add(router);
                    }
                }
                list2 = list;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return list2;
        }

        public static string GetTaskTypeName(object obj)
        {
            string text2;
            try
            {
                if (obj == DBNull.Value)
                {
                    return "";
                }
                int num = (int) obj;
                string text = "";
                switch (num)
                {
                    case 0:
                        text = "一般节点";
                        break;

                    case 1:
                        text = "开始";
                        break;

                    case 2:
                        text = "结束";
                        break;

                    case 3:
                        text = "并流起点";
                        break;

                    case 4:
                        text = "并流交点";
                        break;

                    case 5:
                        text = "会签节点";
                        break;
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return text2;
        }

        private static Procedure LoadProcedureDefinition(string procedureCode)
        {
            Procedure procedure2;
            try
            {
                DataSet set = InterfaceManager.iDefinition.InputDefinition(procedureCode);
                DataTable table = set.Tables["WorkFlowProcedure"];
                if (table.Rows.Count == 0)
                {
                    return null;
                }
                DataRow dr = table.Rows[0];
                Procedure procedure = new Procedure();
                FillProcedure(procedure, dr);
                foreach (DataRow row2 in set.Tables["WorkFlowTask"].Rows)
                {
                    Task task = new Task();
                    FillTask(task, row2);
                    foreach (DataRow row3 in set.Tables["WorkFlowTaskActor"].Select(string.Format("TaskCode='{0}'", task.TaskCode)))
                    {
                        TaskActor taskActor = new TaskActor();
                        FillTaskActor(taskActor, row3);
                        task.AddNewTaskActor(taskActor);
                    }
                    procedure.AddNewTask(task);
                }
                foreach (DataRow row4 in set.Tables["WorkFlowRouter"].Rows)
                {
                    Router router = new Router();
                    FillRouter(router, row4);
                    foreach (DataRow row5 in set.Tables["WorkFlowCondition"].Select(string.Format("RouterCode='{0}'", router.RouterCode)))
                    {
                        Condition condition = new Condition();
                        FillCondition(condition, row5);
                        router.AddNewCondition(condition);
                    }
                    procedure.AddNewRouter(router);
                }
                foreach (DataRow row6 in set.Tables["WorkFlowRole"].Rows)
                {
                    Role roleCase = new Role();
                    FillRole(roleCase, row6);
                    foreach (DataRow row7 in set.Tables["WorkFlowRoleComprise"].Select(string.Format(" WorkFlowRoleCode='{0}'", roleCase.WorkFlowRoleCode)))
                    {
                        RoleComprise roleCompriseCase = new RoleComprise();
                        FillRoleComprise(roleCompriseCase, row7);
                        roleCase.AddNewRoleComprise(roleCompriseCase);
                    }
                    procedure.AddNewRole(roleCase);
                }
                foreach (DataRow row8 in set.Tables["WorkFlowProcedureProperty"].Rows)
                {
                    Property propertyCase = new Property();
                    FillProperty(propertyCase, row8);
                    procedure.AddNewProperty(propertyCase);
                }
                procedure2 = procedure;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return procedure2;
        }

        public static Condition NewCondition()
        {
            Condition condition2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("ConditionCode");
                Condition condition = new Condition();
                condition.ConditionCode = newSysetmCode;
                condition2 = condition;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return condition2;
        }

        public static Procedure NewProcedure()
        {
            Procedure procedure2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("ProcedureCode");
                Procedure procedure = new Procedure();
                procedure.ProcedureCode = newSysetmCode;
                procedure2 = procedure;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return procedure2;
        }

        public static Property NewProperty()
        {
            Property property2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("PropertyCode");
                Property property = new Property();
                property.WorkFlowProcedurePropertyCode = newSysetmCode;
                property2 = property;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return property2;
        }

        public static Role NewRole()
        {
            Role role2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("RoleCode");
                Role role = new Role();
                role.WorkFlowRoleCode = newSysetmCode;
                role2 = role;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return role2;
        }

        public static RoleComprise NewRoleComprise()
        {
            RoleComprise comprise2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("RoleCompriseCode");
                RoleComprise comprise = new RoleComprise();
                comprise.WorkFlowRoleCompriseCode = newSysetmCode;
                comprise2 = comprise;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return comprise2;
        }

        public static Router NewRouter()
        {
            Router router2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("RouterCode");
                Router router = new Router();
                router.RouterCode = newSysetmCode;
                router2 = router;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return router2;
        }

        public static Task NewTask()
        {
            Task task2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("TaskCode");
                Task task = new Task();
                task.TaskCode = newSysetmCode;
                task2 = task;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return task2;
        }

        public static TaskActor NewTaskActor()
        {
            TaskActor actor2;
            try
            {
                string newSysetmCode = InterfaceManager.iSystemCode.GetNewSysetmCode("TaskActorCode");
                TaskActor actor = new TaskActor();
                actor.TaskActorCode = newSysetmCode;
                actor2 = actor;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return actor2;
        }

        public static void RemoveProcedureDefinition(string procedureCode)
        {
            try
            {
                if (m_ProcedureDefinitions.Contains(procedureCode))
                {
                    m_ProcedureDefinitions.Remove(procedureCode);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
        }

        public static DataSet SaveAsProcedureDefinitionData(Procedure procedure, string NewProcedureCode)
        {
            DataSet set2;
            try
            {
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("OldCode", Type.GetType("System.String"));
                DataColumn column2 = new DataColumn("NewCode", Type.GetType("System.String"));
                DataColumn column3 = new DataColumn("CodeType", Type.GetType("System.String"));
                table.Columns.Add(column);
                table.Columns.Add(column2);
                table.Columns.Add(column3);
                DataSet set = InterfaceManager.iDefinition.InputDefinition(procedure.ProcedureCode);
                DeleteAllTableRow(set.Tables["WorkFlowCondition"]);
                DeleteAllTableRow(set.Tables["WorkFlowTaskActor"]);
                DeleteAllTableRow(set.Tables["WorkFlowTask"]);
                DeleteAllTableRow(set.Tables["WorkFlowRouter"]);
                DeleteAllTableRow(set.Tables["WorkFlowRole"]);
                DeleteAllTableRow(set.Tables["WorkFlowRoleComprise"]);
                DeleteAllTableRow(set.Tables["WorkFlowProcedureProperty"]);
                DeleteAllTableRow(set.Tables["WorkFlowProcedure"]);
                DataRow dr = set.Tables["WorkFlowProcedure"].NewRow();
                procedure.ProcedureCode = NewProcedureCode;
                procedure.VersionNumber += 1M;
                procedure.VersionDescription = "";
                procedure.Activity = 0;
                procedure.Description = procedure.Description + DateTime.Now.ToShortDateString();
                WriteProcedure(procedure, dr);
                set.Tables["WorkFlowProcedure"].Rows.Add(dr);
                IDictionaryEnumerator roleEnumerator = procedure.GetRoleEnumerator();
                while (roleEnumerator.MoveNext())
                {
                    Role roleCase = (Role) roleEnumerator.Value;
                    if (roleCase.RoleType == "0")
                    {
                        DataRow row2 = set.Tables["WorkFlowRole"].NewRow();
                        DataRow row = table.NewRow();
                        row["CodeType"] = "RoleCode";
                        row["OldCode"] = roleCase.WorkFlowRoleCode;
                        roleCase.WorkFlowRoleCode = InterfaceManager.iSystemCode.GetNewSysetmCode("RoleCode");
                        roleCase.ProcedureCode = procedure.ProcedureCode;
                        row["NewCode"] = roleCase.WorkFlowRoleCode;
                        table.Rows.Add(row);
                        WriteRole(roleCase, row2);
                        set.Tables["WorkFlowRole"].Rows.Add(row2);
                        IDictionaryEnumerator roleCompriseEnumerator = roleCase.GetRoleCompriseEnumerator();
                        while (roleCompriseEnumerator.MoveNext())
                        {
                            RoleComprise roleCompriseCase = (RoleComprise) roleCompriseEnumerator.Value;
                            DataRow row4 = set.Tables["WorkFlowRoleComprise"].NewRow();
                            roleCompriseCase.WorkFlowRoleCompriseCode = InterfaceManager.iSystemCode.GetNewSysetmCode("RoleCompriseCode");
                            roleCompriseCase.ProcedureCode = procedure.ProcedureCode;
                            roleCompriseCase.RoleCode = roleCase.WorkFlowRoleCode;
                            WriteRoleComprise(roleCompriseCase, row4);
                            set.Tables["WorkFlowRoleComprise"].Rows.Add(row4);
                        }
                    }
                }
                IDictionaryEnumerator propertyEnumerator = procedure.GetPropertyEnumerator();
                while (propertyEnumerator.MoveNext())
                {
                    Property propertyCase = (Property) propertyEnumerator.Value;
                    DataRow row5 = set.Tables["WorkFlowProcedureProperty"].NewRow();
                    DataRow row6 = table.NewRow();
                    row6["CodeType"] = "PropertyCode";
                    row6["OldCode"] = propertyCase.WorkFlowProcedurePropertyCode;
                    propertyCase.WorkFlowProcedurePropertyCode = InterfaceManager.iSystemCode.GetNewSysetmCode("PropertyCode");
                    propertyCase.ProcedureCode = procedure.ProcedureCode;
                    row6["NewCode"] = propertyCase.WorkFlowProcedurePropertyCode;
                    table.Rows.Add(row6);
                    WriteProperty(propertyCase, row5);
                    set.Tables["WorkFlowProcedureProperty"].Rows.Add(row5);
                }
                IDictionaryEnumerator taskEnumerator = procedure.GetTaskEnumerator();
                while (taskEnumerator.MoveNext())
                {
                    Task task = (Task) taskEnumerator.Value;
                    DataRow row7 = set.Tables["WorkFlowTask"].NewRow();
                    DataRow row8 = table.NewRow();
                    row8["CodeType"] = "TaskCode";
                    row8["OldCode"] = task.TaskCode;
                    task.TaskCode = InterfaceManager.iSystemCode.GetNewSysetmCode("TaskCode");
                    task.ProcedureCode = procedure.ProcedureCode;
                    task.TaskProperty = (task.TaskProperty == "") ? "" : table.Select("OldCode='" + task.TaskProperty + "' and CodeType='PropertyCode'")[0]["NewCode"].ToString();
                    foreach (DataRow row9 in table.Select("OldCode='" + task.TaskRole + "' and CodeType='RoleCode'"))
                    {
                        task.TaskRole = row9["NewCode"].ToString();
                    }
                    row8["NewCode"] = task.TaskCode;
                    table.Rows.Add(row8);
                    WriteTask(task, row7);
                    set.Tables["WorkFlowTask"].Rows.Add(row7);
                    IDictionaryEnumerator taskActorEnumerator = task.GetTaskActorEnumerator();
                    while (taskActorEnumerator.MoveNext())
                    {
                        TaskActor taskActor = (TaskActor) taskActorEnumerator.Value;
                        DataRow row10 = set.Tables["WorkFlowTaskActor"].NewRow();
                        taskActor.TaskActorCode = InterfaceManager.iSystemCode.GetNewSysetmCode("TaskActorCode");
                        taskActor.ProcedureCode = procedure.ProcedureCode;
                        taskActor.TaskCode = task.TaskCode;
                        taskActor.ActorProperty = (taskActor.ActorProperty == "") ? "" : table.Select("OldCode='" + taskActor.ActorProperty + "' and CodeType='PropertyCode'")[0]["NewCode"].ToString();
                        foreach (DataRow row11 in table.Select("OldCode='" + taskActor.ActorCode + "' and CodeType='RoleCode'"))
                        {
                            taskActor.ActorCode = row11["NewCode"].ToString();
                        }
                        WriteTaskActor(taskActor, row10);
                        set.Tables["WorkFlowTaskActor"].Rows.Add(row10);
                    }
                }
                IDictionaryEnumerator routerEnumerator = procedure.GetRouterEnumerator();
                while (routerEnumerator.MoveNext())
                {
                    Router router = (Router) routerEnumerator.Value;
                    DataRow row12 = set.Tables["WorkFlowRouter"].NewRow();
                    router.RouterCode = InterfaceManager.iSystemCode.GetNewSysetmCode("RouterCode");
                    router.ProcedureCode = procedure.ProcedureCode;
                    router.FromTaskCode = table.Select("OldCode='" + router.FromTaskCode + "' and CodeType='TaskCode'")[0]["NewCode"].ToString();
                    router.ToTaskCode = table.Select("OldCode='" + router.ToTaskCode + "' and CodeType='TaskCode'")[0]["NewCode"].ToString();
                    WriteRouter(router, row12);
                    set.Tables["WorkFlowRouter"].Rows.Add(row12);
                    IDictionaryEnumerator conditionEnumerator = router.GetConditionEnumerator();
                    while (conditionEnumerator.MoveNext())
                    {
                        Condition condition = (Condition) conditionEnumerator.Value;
                        DataRow row13 = set.Tables["WorkFlowCondition"].NewRow();
                        condition.ConditionCode = InterfaceManager.iSystemCode.GetNewSysetmCode("ConditionCode");
                        condition.ProcedureCode = procedure.ProcedureCode;
                        condition.RouterCode = router.RouterCode;
                        WriteCondition(condition, row13);
                        set.Tables["WorkFlowCondition"].Rows.Add(row13);
                    }
                }
                set2 = set;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return set2;
        }

        public static void SaveAsProcedureDifinition(Procedure procedure)
        {
            try
            {
                string newProcedureCode = InterfaceManager.iSystemCode.GetNewSysetmCode("ProcedureCode");
                DataSet ds = SaveAsProcedureDefinitionData(procedure, newProcedureCode);
                InterfaceManager.iDefinition.OutputDefinition(ds, newProcedureCode);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
        }

        public static DataSet SaveProcedureDefinitionData(Procedure procedure)
        {
            DataSet set2;
            try
            {
                DataSet set = InterfaceManager.iDefinition.InputDefinition(procedure.ProcedureCode);
                DeleteAllTableRow(set.Tables["WorkFlowCondition"]);
                DeleteAllTableRow(set.Tables["WorkFlowTaskActor"]);
                DeleteAllTableRow(set.Tables["WorkFlowTask"]);
                DeleteAllTableRow(set.Tables["WorkFlowRouter"]);
                DeleteAllTableRow(set.Tables["WorkFlowRole"]);
                DeleteAllTableRow(set.Tables["WorkFlowRoleComprise"]);
                DeleteAllTableRow(set.Tables["WorkFlowProcedureProperty"]);
                DeleteAllTableRow(set.Tables["WorkFlowProcedure"]);
                DataRow dr = set.Tables["WorkFlowProcedure"].NewRow();
                WriteProcedure(procedure, dr);
                set.Tables["WorkFlowProcedure"].Rows.Add(dr);
                IDictionaryEnumerator taskEnumerator = procedure.GetTaskEnumerator();
                while (taskEnumerator.MoveNext())
                {
                    Task task = (Task) taskEnumerator.Value;
                    DataRow row2 = set.Tables["WorkFlowTask"].NewRow();
                    WriteTask(task, row2);
                    set.Tables["WorkFlowTask"].Rows.Add(row2);
                    IDictionaryEnumerator taskActorEnumerator = task.GetTaskActorEnumerator();
                    while (taskActorEnumerator.MoveNext())
                    {
                        TaskActor taskActor = (TaskActor) taskActorEnumerator.Value;
                        DataRow row3 = set.Tables["WorkFlowTaskActor"].NewRow();
                        WriteTaskActor(taskActor, row3);
                        set.Tables["WorkFlowTaskActor"].Rows.Add(row3);
                    }
                }
                IDictionaryEnumerator routerEnumerator = procedure.GetRouterEnumerator();
                while (routerEnumerator.MoveNext())
                {
                    Router router = (Router) routerEnumerator.Value;
                    DataRow row4 = set.Tables["WorkFlowRouter"].NewRow();
                    WriteRouter(router, row4);
                    set.Tables["WorkFlowRouter"].Rows.Add(row4);
                    IDictionaryEnumerator conditionEnumerator = router.GetConditionEnumerator();
                    while (conditionEnumerator.MoveNext())
                    {
                        Condition condition = (Condition) conditionEnumerator.Value;
                        DataRow row5 = set.Tables["WorkFlowCondition"].NewRow();
                        WriteCondition(condition, row5);
                        set.Tables["WorkFlowCondition"].Rows.Add(row5);
                    }
                }
                IDictionaryEnumerator roleEnumerator = procedure.GetRoleEnumerator();
                while (roleEnumerator.MoveNext())
                {
                    Role roleCase = (Role) roleEnumerator.Value;
                    DataRow row6 = set.Tables["WorkFlowRole"].NewRow();
                    WriteRole(roleCase, row6);
                    set.Tables["WorkFlowRole"].Rows.Add(row6);
                    IDictionaryEnumerator roleCompriseEnumerator = roleCase.GetRoleCompriseEnumerator();
                    while (roleCompriseEnumerator.MoveNext())
                    {
                        RoleComprise roleCompriseCase = (RoleComprise) roleCompriseEnumerator.Value;
                        DataRow row7 = set.Tables["WorkFlowRoleComprise"].NewRow();
                        WriteRoleComprise(roleCompriseCase, row7);
                        set.Tables["WorkFlowRoleComprise"].Rows.Add(row7);
                    }
                }
                IDictionaryEnumerator propertyEnumerator = procedure.GetPropertyEnumerator();
                while (propertyEnumerator.MoveNext())
                {
                    Property propertyCase = (Property) propertyEnumerator.Value;
                    DataRow row8 = set.Tables["WorkFlowProcedureProperty"].NewRow();
                    WriteProperty(propertyCase, row8);
                    set.Tables["WorkFlowProcedureProperty"].Rows.Add(row8);
                }
                set2 = set;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            return set2;
        }

        public static void SaveProcedureDifinition(Procedure procedure)
        {
            try
            {
                RemoveProcedureDefinition(procedure.ProcedureCode);
                InterfaceManager.iDefinition.OutputDefinition(SaveProcedureDefinitionData(procedure), procedure.ProcedureCode);
                m_ProcedureDefinitions.Add(procedure.ProcedureCode, procedure);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
        }

        private static void WriteCondition(Condition condition, DataRow dr)
        {
            dr["ConditionCode"] = condition.ConditionCode;
            dr["RouterCode"] = condition.RouterCode;
            dr["ProcedureCode"] = condition.ProcedureCode;
            dr["ConditionType"] = condition.ConditionType;
            dr["Description"] = condition.Description;
        }

        private static void WriteProcedure(Procedure procedure, DataRow dr)
        {
            dr["ProcedureCode"] = procedure.ProcedureCode;
            dr["ProcedureName"] = procedure.ProcedureName;
            dr["Description"] = procedure.Description;
            dr["ApplicationPath"] = procedure.ApplicationPath;
            dr["ApplicationInfoPath"] = procedure.ApplicationInfoPath;
            dr["Type"] = procedure.Type;
            dr["SysType"] = procedure.SysType;
            dr["Remark"] = procedure.Remark;
            dr["VersionNumber"] = procedure.VersionNumber;
            dr["ProjectCode"] = procedure.ProjectCode;
            dr["CreateUser"] = procedure.CreateUser;
            dr["CreateDate"] = procedure.CreateDate;
            dr["ModifyUser"] = procedure.ModifyUser;
            dr["ModifyDate"] = procedure.ModifyDate;
            dr["Activity"] = procedure.Activity;
            dr["VersionDescription"] = procedure.VersionDescription;
            dr["ProcedureRemark"] = procedure.ProcedureRemark;
        }

        private static void WriteProperty(Property PropertyCase, DataRow dr)
        {
            dr["WorkFlowProcedurePropertyCode"] = PropertyCase.WorkFlowProcedurePropertyCode;
            dr["ProcedureCode"] = PropertyCase.ProcedureCode;
            dr["ProcedurePropertyName"] = PropertyCase.ProcedurePropertyName;
            dr["ProcedurePropertyType"] = PropertyCase.ProcedurePropertyType;
            dr["Remak"] = PropertyCase.Remak;
        }

        private static void WriteRole(Role RoleCase, DataRow dr)
        {
            dr["WorkFlowRoleCode"] = RoleCase.WorkFlowRoleCode;
            dr["RoleName"] = RoleCase.RoleName;
            dr["RoleType"] = RoleCase.RoleType;
            dr["ProcedureCode"] = RoleCase.ProcedureCode;
            dr["IsAllUser"] = RoleCase.IsAllUser;
            dr["Remak"] = RoleCase.Remak;
        }

        private static void WriteRoleComprise(RoleComprise RoleCompriseCase, DataRow dr)
        {
            dr["WorkFlowRoleCompriseCode"] = RoleCompriseCase.WorkFlowRoleCompriseCode;
            dr["ProcedureCode"] = RoleCompriseCase.ProcedureCode;
            dr["WorkFlowRoleCode"] = RoleCompriseCase.RoleCode;
            dr["RoleComprise"] = RoleCompriseCase.RoleCompriseItem;
            dr["RoleType"] = RoleCompriseCase.RoleType.ToString();
        }

        private static void WriteRouter(Router router, DataRow dr)
        {
            dr["RouterCode"] = router.RouterCode;
            dr["ProcedureCode"] = router.ProcedureCode;
            dr["FromTaskCode"] = router.FromTaskCode;
            dr["ToTaskCode"] = router.ToTaskCode;
            dr["Description"] = router.Description;
            dr["ToHandle"] = router.ToHandle;
            dr["SoftID"] = router.SoftID;
            dr["SortID"] = router.SortID;
        }

        private static void WriteTask(Task task, DataRow dr)
        {
            dr["TaskCode"] = task.TaskCode;
            dr["ProcedureCode"] = task.ProcedureCode;
            dr["TaskID"] = task.TaskID;
            dr["TaskName"] = task.TaskName;
            dr["Description"] = task.Description;
            dr["TaskType"] = task.TaskType;
            dr["TaskActorType"] = task.TaskActorType;
            dr["WayOfSelectPerson"] = task.WayOfSelectPerson;
            dr["IsOrderly"] = task.IsOrderly;
            dr["CanManual"] = task.CanManual;
            dr["CanEdit"] = task.CanEdit;
            dr["IsFinish"] = task.IsFinish;
            dr["HasOpinion"] = task.HasOpinion;
            dr["SortID"] = task.SortID;
            dr["Copy"] = task.Copy;
            dr["ModuleState"] = task.ModuleState;
            dr["TaskProperty"] = task.TaskProperty;
            dr["TaskTitle"] = task.TaskTitle;
            dr["TaskRole"] = task.TaskRole;
            dr["TaskMeetType"] = task.TaskMeetType;
            dr["OpinionType"] = task.OpinionType;
        }

        private static void WriteTaskActor(TaskActor taskActor, DataRow dr)
        {
            dr["TaskActorCode"] = taskActor.TaskActorCode;
            dr["TaskCode"] = taskActor.TaskCode;
            dr["ProcedureCode"] = taskActor.ProcedureCode;
            dr["ActorCode"] = taskActor.ActorCode;
            dr["ActorType"] = taskActor.ActorType;
            dr["IOrder"] = taskActor.IOrder;
            dr["TaskActorName"] = taskActor.TaskActorName;
            dr["TaskActorID"] = taskActor.TaskActorID;
            dr["ActorProperty"] = taskActor.ActorProperty;
            dr["ActorNeed"] = taskActor.ActorNeed;
            dr["ActorModuleState"] = taskActor.ActorModuleState;
            dr["TaskActorType"] = taskActor.TaskActorType;
            dr["OpinionType"] = taskActor.OpinionType;
        }
    }
}

