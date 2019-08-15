namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class WorkFlowInOut
    {
        protected static string ConnectionString = ConfigurationManager.AppSettings["DBConnString"];
        private DataSet myds = new DataSet();
        private SqlDataAdapter mysda = new SqlDataAdapter();
        private SqlCommand sqcomd = new SqlCommand();

        public void BackDataToXml(string path, string xmlName)
        {
            this.ReadAll();
            WriteToServer(this.myds, path, xmlName);
        }

        public void BackDataToXml(string path, string xmlName, string ProcedureCodeStr)
        {
            this.ReadAll(ProcedureCodeStr);
            WriteToServer(this.myds, path, xmlName);
        }

        public void BackRoleDataToXml(string path, string xmlName)
        {
            this.ReadRole();
            WriteToServer(this.myds, path, xmlName);
        }

        protected static SqlConnection ConnectDataBase()
        {
            return new SqlConnection(ConnectionString);
        }

        public void DeleteTable(string tableName)
        {
            ExecWorkFlowSQL.ExecuteSqlFromDB(WorkFlowSQL.DeleteAllWorkFlow(tableName));
        }

        protected object GetMaxCodeValue(DataSet ds, string tableCode)
        {
            DataTable table = ds.Tables[0];
            int count = table.Rows.Count;
            if (count > 0)
            {
                DataRow row = table.Rows[count - 1];
                return row[tableCode];
            }
            return 1;
        }

        protected object MaxCodeValue(DataTable dt, string tableCode)
        {
            int count = dt.Rows.Count;
            if (count > 0)
            {
                DataRow row = dt.Rows[count - 1];
                return row[tableCode];
            }
            return 0;
        }

        public void OverWriteAllWorkFlow(string path, string newPath)
        {
            this.OverWriteDB(path, newPath, WorkFlowTableName.WorkFlowCondition.ToString(), WorkFlowTableCode.ConditionCode.ToString());
            this.OverWriteDB(path, newPath, WorkFlowTableName.WorkFlowProcedure.ToString(), WorkFlowTableCode.ProcedureCode.ToString());
            this.OverWriteDB(path, newPath, WorkFlowTableName.WorkFlowRoleComprise.ToString(), WorkFlowTableCode.WorkFlowRoleCompriseCode.ToString());
            this.OverWriteDB(path, newPath, WorkFlowTableName.WorkFlowProcedureProperty.ToString(), WorkFlowTableCode.WorkFlowProcedurePropertyCode.ToString());
            this.OverWriteDB(path, newPath, WorkFlowTableName.WorkFlowRole.ToString(), WorkFlowTableCode.WorkFlowRoleCode.ToString());
            this.OverWriteDB(path, newPath, WorkFlowTableName.WorkFlowRouter.ToString(), WorkFlowTableCode.RouterCode.ToString());
            this.OverWriteDB(path, newPath, WorkFlowTableName.WorkFlowTask.ToString(), WorkFlowTableCode.TaskCode.ToString());
            this.OverWriteDB(path, newPath, WorkFlowTableName.WorkFlowTaskActor.ToString(), WorkFlowTableCode.TaskActorCode.ToString());
        }

        public void OverWriteDB(string path, string newPath, string tableName, string tableCode)
        {
            this.ReadOldData(path, tableName, tableCode);
            this.SetNewData(newPath, tableName);
            this.SetNewSysCodeByCode(tableName, this.GetMaxCodeValue(this.myds, tableCode));
        }

        public void ReadAll()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowProcedure.ToString(), WorkFlowTableCode.ProcedureCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowProcedureProperty.ToString(), WorkFlowTableCode.WorkFlowProcedurePropertyCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowRole.ToString(), WorkFlowTableCode.WorkFlowRoleCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowRoleComprise.ToString(), WorkFlowTableCode.WorkFlowRoleCompriseCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowRouter.ToString(), WorkFlowTableCode.RouterCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowCondition.ToString(), WorkFlowTableCode.ConditionCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowTask.ToString(), WorkFlowTableCode.TaskCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowTaskActor.ToString(), WorkFlowTableCode.TaskActorCode.ToString()) + " ");
            this.sqcomd.CommandText = builder.ToString();
            this.sqcomd.Connection = ConnectDataBase();
            this.mysda.SelectCommand = this.sqcomd;
            this.mysda.Fill(this.myds);
            for (int i = 0; i < this.myds.Tables.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this.myds.Tables[0].TableName = WorkFlowTableName.WorkFlowProcedure.ToString();
                        break;

                    case 1:
                        this.myds.Tables[1].TableName = WorkFlowTableName.WorkFlowProcedureProperty.ToString();
                        break;

                    case 2:
                        this.myds.Tables[2].TableName = WorkFlowTableName.WorkFlowRole.ToString();
                        break;

                    case 3:
                        this.myds.Tables[3].TableName = WorkFlowTableName.WorkFlowRoleComprise.ToString();
                        break;

                    case 4:
                        this.myds.Tables[4].TableName = WorkFlowTableName.WorkFlowRouter.ToString();
                        break;

                    case 5:
                        this.myds.Tables[5].TableName = WorkFlowTableName.WorkFlowCondition.ToString();
                        break;

                    case 6:
                        this.myds.Tables[6].TableName = WorkFlowTableName.WorkFlowTask.ToString();
                        break;

                    case 7:
                        this.myds.Tables[7].TableName = WorkFlowTableName.WorkFlowTaskActor.ToString();
                        break;
                }
            }
        }

        public void ReadAll(string ProcedureCodeStr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowProcedure.ToString() + " where ProcedureCode in (" + ProcedureCodeStr + ") ", WorkFlowTableCode.ProcedureCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowProcedureProperty.ToString() + " where ProcedureCode in (" + ProcedureCodeStr + ") ", WorkFlowTableCode.WorkFlowProcedurePropertyCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowRole.ToString() + " where ProcedureCode in (" + ProcedureCodeStr + ") and RoleType='0'", WorkFlowTableCode.WorkFlowRoleCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowRoleComprise.ToString() + " where ProcedureCode in (" + ProcedureCodeStr + ") and workflowrolecode in (select workflowrolecode from workflowrole where RoleType='0')", WorkFlowTableCode.WorkFlowRoleCompriseCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowRouter.ToString() + " where ProcedureCode in (" + ProcedureCodeStr + ") ", WorkFlowTableCode.RouterCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowCondition.ToString() + " where ProcedureCode in (" + ProcedureCodeStr + ") ", WorkFlowTableCode.ConditionCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowTask.ToString() + " where ProcedureCode in (" + ProcedureCodeStr + ") ", WorkFlowTableCode.TaskCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowTaskActor.ToString() + " where ProcedureCode in (" + ProcedureCodeStr + ") ", WorkFlowTableCode.TaskActorCode.ToString()) + " ");
            this.sqcomd.CommandText = builder.ToString();
            this.sqcomd.Connection = ConnectDataBase();
            this.mysda.SelectCommand = this.sqcomd;
            this.mysda.Fill(this.myds);
            for (int i = 0; i < this.myds.Tables.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this.myds.Tables[0].TableName = WorkFlowTableName.WorkFlowProcedure.ToString();
                        break;

                    case 1:
                        this.myds.Tables[1].TableName = WorkFlowTableName.WorkFlowProcedureProperty.ToString();
                        break;

                    case 2:
                        this.myds.Tables[2].TableName = WorkFlowTableName.WorkFlowRole.ToString();
                        break;

                    case 3:
                        this.myds.Tables[3].TableName = WorkFlowTableName.WorkFlowRoleComprise.ToString();
                        break;

                    case 4:
                        this.myds.Tables[4].TableName = WorkFlowTableName.WorkFlowRouter.ToString();
                        break;

                    case 5:
                        this.myds.Tables[5].TableName = WorkFlowTableName.WorkFlowCondition.ToString();
                        break;

                    case 6:
                        this.myds.Tables[6].TableName = WorkFlowTableName.WorkFlowTask.ToString();
                        break;

                    case 7:
                        this.myds.Tables[7].TableName = WorkFlowTableName.WorkFlowTaskActor.ToString();
                        break;
                }
            }
        }

        public static DataSet ReadFromXml(string path, string xmlName)
        {
            DataSet set = new DataSet();
            set.ReadXml(path + xmlName + ".xml");
            return set;
        }

        protected void ReadOldData(string path, string tableName, string tableCode)
        {
            this.sqcomd.CommandText = WorkFlowSQL.SeleAllWorkFlow(tableName, tableCode);
            this.sqcomd.Connection = ConnectDataBase();
            this.mysda.SelectCommand = this.sqcomd;
            this.mysda.Fill(this.myds);
            WriteToServer(this.myds, path, tableName);
        }

        public void ReadRole()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowRole.ToString() + " where RoleType='1'", WorkFlowTableCode.WorkFlowRoleCode.ToString()) + " ");
            builder.Append(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowRoleComprise.ToString() + " where workflowrolecode in (select workflowrolecode from workflowrole where RoleType='1')", WorkFlowTableCode.WorkFlowRoleCompriseCode.ToString()) + " ");
            this.sqcomd.CommandText = builder.ToString();
            this.sqcomd.Connection = ConnectDataBase();
            this.mysda.SelectCommand = this.sqcomd;
            this.mysda.Fill(this.myds);
            for (int i = 0; i < this.myds.Tables.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this.myds.Tables[0].TableName = WorkFlowTableName.WorkFlowRole.ToString();
                        break;

                    case 1:
                        this.myds.Tables[1].TableName = WorkFlowTableName.WorkFlowRoleComprise.ToString();
                        break;
                }
            }
        }

        public static DataSet ReadWorkFlowConditionFromDB()
        {
            DataSet set2;
            QueryAgent agent = new QueryAgent();
            string queryString = "select * from WorkFlowCondition order by ConditionCode";
            try
            {
                DataSet set = new DataSet("WorkFlowCondition");
                set2 = agent.ExecSqlForDataSet(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public static DataSet ReadWorkFlowProcedureFromDB()
        {
            DataSet set2;
            QueryAgent agent = new QueryAgent();
            string queryString = "select * from WorkFlowProcedure order by ProcedureCode";
            try
            {
                DataSet set = new DataSet("WorkFlowProcedure");
                set2 = agent.ExecSqlForDataSet(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public static DataSet ReadWorkFlowProcedurePropertyFromDB()
        {
            DataSet set2;
            QueryAgent agent = new QueryAgent();
            string queryString = "select * from WorkFlowProcedureProperty order by WorkFlowProcedurePropertyCode";
            try
            {
                DataSet set = new DataSet("WorkFlowProcedureProperty");
                set2 = agent.ExecSqlForDataSet(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public static DataSet ReadWorkFlowRoleCompriseFromDB()
        {
            DataSet set2;
            QueryAgent agent = new QueryAgent();
            string queryString = "select * from WorkFlowRoleComprise order by WorkFlowRoleCompriseCode";
            try
            {
                DataSet set = new DataSet("WorkFlowRoleComprise");
                set2 = agent.ExecSqlForDataSet(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public static DataSet ReadWorkFlowRoleFromDB()
        {
            DataSet set2;
            QueryAgent agent = new QueryAgent();
            string queryString = "select * from WorkFlowRole order by WorkFlowRoleCode";
            try
            {
                DataSet set = new DataSet("WorkFlowRole");
                set2 = agent.ExecSqlForDataSet(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public static DataSet ReadWorkFlowRouterFromDB()
        {
            DataSet set2;
            QueryAgent agent = new QueryAgent();
            string queryString = "select * from WorkFlowRouter order by RouterCode";
            try
            {
                DataSet set = new DataSet("WorkFlowRouter");
                set2 = agent.ExecSqlForDataSet(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public string ReturnSyscodeName(string tableName)
        {
            switch (tableName)
            {
                case "WorkFlowTaskActor":
                    return "taskActorcode";

                case "WorkFlowTask":
                    return "taskcode";

                case "WorkFlowRouter":
                    return "routercode";

                case "WorkFlowRoleComprise":
                    return "RoleCompriseCode";

                case "WorkFlowRole":
                    return "RoleCode";

                case "WorkFlowProcedureProperty":
                    return "propertycode";

                case "WorkFlowProcedure":
                    return "procedurecode";

                case "WorkFlowCondition":
                    return "ConditionCode";
            }
            return "";
        }

        public static void SearchWorkFlowProcedureFromDB(DataSet ds)
        {
        }

        public DataSet SetNewData(string newPath, string tableName)
        {
            this.DeleteTable(tableName);
            this.myds.Clear();
            SqlCommandBuilder builder = new SqlCommandBuilder(this.mysda);
            this.mysda.InsertCommand = builder.GetInsertCommand();
            this.mysda.DeleteCommand = builder.GetDeleteCommand();
            this.mysda.UpdateCommand = builder.GetUpdateCommand();
            this.myds.ReadXml(newPath + tableName + ".xml");
            this.mysda.Update(this.myds);
            return this.myds;
        }

        public void SetNewDataSet(string newpath, string oldpath, string xmlName)
        {
            this.SetNewDataSet(newpath, oldpath, xmlName, true);
        }

        public void SetNewDataSet(string newpath, string oldpath, string xmlName, bool IsAll)
        {
            DataSet set = new DataSet();
            set.ReadXml(oldpath + xmlName);
            if (set.Tables.Count != 8)
            {
                throw new Exception("非法的导入,请检查导入文件格式!");
            }
            set.Dispose();
            this.BackDataToXml(oldpath, xmlName + "x");
            this.myds.Clear();
            this.myds.ReadXml(newpath + xmlName);
            string tableName = null;
            string text2 = null;
            string tableCode = null;
            for (int i = 0; i < this.myds.Tables.Count; i++)
            {
                switch (this.myds.Tables[i].TableName)
                {
                    case "WorkFlowProcedure":
                        text2 = WorkFlowTableName.WorkFlowProcedure.ToString();
                        tableCode = WorkFlowTableCode.ProcedureCode.ToString();
                        break;

                    case "WorkFlowProcedureProperty":
                        text2 = WorkFlowTableName.WorkFlowProcedureProperty.ToString();
                        tableCode = WorkFlowTableCode.WorkFlowProcedurePropertyCode.ToString();
                        break;

                    case "WorkFlowRole":
                        text2 = WorkFlowTableName.WorkFlowRole.ToString();
                        tableCode = WorkFlowTableCode.WorkFlowRoleCode.ToString();
                        break;

                    case "WorkFlowRoleComprise":
                        text2 = WorkFlowTableName.WorkFlowRoleComprise.ToString();
                        tableCode = WorkFlowTableCode.WorkFlowRoleCompriseCode.ToString();
                        break;

                    case "WorkFlowRouter":
                        text2 = WorkFlowTableName.WorkFlowRouter.ToString();
                        tableCode = WorkFlowTableCode.RouterCode.ToString();
                        break;

                    case "WorkFlowCondition":
                        text2 = WorkFlowTableName.WorkFlowCondition.ToString();
                        tableCode = WorkFlowTableCode.ConditionCode.ToString();
                        break;

                    case "WorkFlowTask":
                        text2 = WorkFlowTableName.WorkFlowTask.ToString();
                        tableCode = WorkFlowTableCode.TaskCode.ToString();
                        break;

                    case "WorkFlowTaskActor":
                        text2 = WorkFlowTableName.WorkFlowTaskActor.ToString();
                        tableCode = WorkFlowTableCode.TaskActorCode.ToString();
                        break;
                }
                tableName = text2;
                if (!IsAll)
                {
                    string text4 = "";
                    foreach (DataRow row in this.myds.Tables[i].Select())
                    {
                        object obj3 = text4;
                        text4 = string.Concat(new object[] { obj3, "'", row[tableCode], "'," });
                    }
                    if (text4 == "")
                    {
                        text2 = text2 + " where " + tableCode + " in ('')";
                    }
                    else
                    {
                        string text6 = text2;
                        text2 = text6 + " where " + tableCode + " in (" + text4.Substring(0, text4.Length - 1) + ") ";
                    }
                }
                this.WriteToDB(this.myds.Tables[i], text2, tableCode, IsAll);
                object code = this.MaxCodeValue(this.myds.Tables[i], tableCode);
                this.SetNewSysCodeByCode(tableName, code);
            }
        }

        public void SetNewRoleDataSet(string newpath, string oldpath, string xmlName, bool IsAll)
        {
            this.BackRoleDataToXml(oldpath, xmlName + "x");
            this.myds.Clear();
            this.myds.ReadXml(newpath + xmlName);
            string tableName = null;
            string text2 = null;
            string tableCode = null;
            for (int i = 0; i < this.myds.Tables.Count; i++)
            {
                switch (this.myds.Tables[i].TableName)
                {
                    case "WorkFlowRole":
                        text2 = WorkFlowTableName.WorkFlowRole.ToString();
                        tableCode = WorkFlowTableCode.WorkFlowRoleCode.ToString();
                        break;

                    case "WorkFlowRoleComprise":
                        text2 = WorkFlowTableName.WorkFlowRoleComprise.ToString();
                        tableCode = WorkFlowTableCode.WorkFlowRoleCompriseCode.ToString();
                        goto Label_00AF;
                }
            Label_00AF:
                tableName = text2;
                if (!IsAll)
                {
                    string text4 = "";
                    foreach (DataRow row in this.myds.Tables[i].Select())
                    {
                        object obj3 = text4;
                        text4 = string.Concat(new object[] { obj3, "'", row[tableCode], "'," });
                    }
                    if (text4 == "")
                    {
                        text2 = text2 + " where " + tableCode + " in ('')";
                    }
                    else
                    {
                        string text6 = text2;
                        text2 = text6 + " where " + tableCode + " in (" + text4.Substring(0, text4.Length - 1) + ") ";
                    }
                }
                this.WriteToDB(this.myds.Tables[i], text2, tableCode, IsAll);
                object code = this.MaxCodeValue(this.myds.Tables[i], tableCode);
                this.SetNewSysCodeByCode(tableName, code);
            }
        }

        public void SetNewSysCodeByCode(string tableName, object code)
        {
            string text = code.ToString();
            string keyvalues = this.ReturnSyscodeName(tableName);
            using (SingleEntityDAO ydao = new SingleEntityDAO("SysCode"))
            {
                EntityData entity = ydao.SelectbyPrimaryKey(keyvalues);
                if (!entity.HasRecord())
                {
                    SystemManageDAO.GetNewSysCode(keyvalues);
                }
                else
                {
                    string text3 = entity.GetString("CodeRule");
                    if ((text3.Length > 0) & !code.Equals(0))
                    {
                        int count = text3.Split("+".ToCharArray())[0].Length;
                        text = text.Remove(0, count).Trim().ToString();
                    }
                }
                int num2 = Convert.ToInt32(text);
                if (num2 <= 0)
                {
                    DataRow newRecord = entity.GetNewRecord();
                    num2 = 0x186a1;
                    entity.CurrentRow["CodeValue"] = num2;
                }
                else if (num2 > ((int) entity.CurrentRow["CodeValue"]))
                {
                    entity.CurrentRow["CodeValue"] = num2;
                }
                SystemManageDAO.UpdateSysCode(entity);
                entity.Dispose();
            }
        }

        public void UpdateSysCode(string tableName, object code)
        {
            this.SetNewSysCodeByCode(tableName, code);
        }

        public static void UpXmlDB(string path, HttpPostedFile UpFile, string xmName)
        {
            try
            {
                if (UpFile.FileName.Trim() == "")
                {
                    throw new Exception("请选择文件");
                }
                string text = UpFile.FileName.ToString();
                UpFile.SaveAs(path + @"\" + xmName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void WriteAllWorkFlow(string path)
        {
            WriteToServer(ReadWorkFlowConditionFromDB(), path, WorkFlowTableName.WorkFlowCondition.ToString());
            WriteToServer(ReadWorkFlowProcedureFromDB(), path, WorkFlowTableName.WorkFlowProcedure.ToString());
            WriteToServer(ReadWorkFlowProcedurePropertyFromDB(), path, WorkFlowTableName.WorkFlowProcedureProperty.ToString());
            WriteToServer(ReadWorkFlowRoleCompriseFromDB(), path, WorkFlowTableName.WorkFlowRoleComprise.ToString());
            WriteToServer(ReadWorkFlowRoleFromDB(), path, WorkFlowTableName.WorkFlowRole.ToString());
            WriteToServer(ReadWorkFlowRouterFromDB(), path, WorkFlowTableName.WorkFlowRouter.ToString());
            WriteToServer(ExecWorkFlowSQL.ExecSqlForDataSetFromDB(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowTask.ToString(), WorkFlowTableCode.TaskCode.ToString())), path, WorkFlowTableName.WorkFlowTask.ToString());
            WriteToServer(ExecWorkFlowSQL.ExecSqlForDataSetFromDB(WorkFlowSQL.SeleAllWorkFlow(WorkFlowTableName.WorkFlowTaskActor.ToString(), WorkFlowTableCode.TaskActorCode.ToString())), path, WorkFlowTableName.WorkFlowTaskActor.ToString());
        }

        protected void WriteToDB(DataTable dt, string tableName, string tableCode)
        {
            this.WriteToDB(dt, tableName, tableCode, true);
        }

        protected void WriteToDB(DataTable dt, string tableName, string tableCode, bool IsALL)
        {
            DataSet set = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            try
            {
                try
                {
                    command.CommandText = WorkFlowSQL.SeleAllWorkFlow(tableName, tableCode);
                    command.Connection = ConnectDataBase();
                    adapter.SelectCommand = command;
                    this.DeleteTable(tableName);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.DeleteCommand = builder.GetDeleteCommand();
                    adapter.UpdateCommand = builder.GetUpdateCommand();
                    adapter.Update(dt);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                set.Dispose();
                adapter.Dispose();
                command.Dispose();
            }
        }

        public static void WriteToServer(DataSet ds, string path)
        {
            ds.WriteXml(path, XmlWriteMode.IgnoreSchema);
        }

        public static void WriteToServer(DataSet ds, string path, string xmlName)
        {
            ds.WriteXml(path + xmlName, XmlWriteMode.IgnoreSchema);
        }

        public class ExecWorkFlowSQL
        {
            public static DataSet ExecSqlForDataSetFromDB(string sql)
            {
                DataSet set2;
                QueryAgent agent = new QueryAgent();
                try
                {
                    DataSet set = new DataSet();
                    set2 = agent.ExecSqlForDataSet(sql);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return set2;
            }

            public static void ExecuteSqlFromDB(string sql)
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    agent.ExecuteSql(sql);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            public static void UpdateDataAdapter(DataSet ds)
            {
                try
                {
                    new SqlDataAdapter().Update(ds);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public class WorkFlowSQL
        {
            public static string DeleteAllWorkFlow(string tableName)
            {
                return ("Delete From " + tableName);
            }

            public static string InsertWorkFlowCondition(string tableName, string ConditionCode, string ProcedureCode, string RouterCode, string ConditionType, string Description)
            {
                return ("insert " + tableName + " (ConditionCode,ProcedureCode,RouterCode,ConditionType,Description) values(" + ConditionCode + "," + ProcedureCode + "," + RouterCode + "," + ConditionType + "," + Description + ")");
            }

            public static string InsertWorkFlowRouter(string tableName, string RouterCode, string ProcedureCode, string FromTaskCode, string ToTaskCode, string Description, string ToHandle, string SoftID, string SortID)
            {
                return ("insert " + tableName + " (tableName,RouterCode,ProcedureCode,FromTaskCode,ToTaskCode,Description,ToHandle,SoftID,SortID) values (" + tableName + "," + RouterCode + "," + ProcedureCode + "," + FromTaskCode + "," + ToTaskCode + "," + Description + "," + ToHandle + "," + SoftID + "," + SortID + ")");
            }

            public static string SeleAllWorkFlow(string tableName, string orderCode)
            {
                return ("Select * From " + tableName + " order by " + orderCode);
            }
        }

        public enum WorkFlowTableCode
        {
            ProcedureCode,
            WorkFlowProcedurePropertyCode,
            WorkFlowRoleCode,
            WorkFlowRoleCompriseCode,
            ConditionCode,
            RouterCode,
            TaskCode,
            TaskActorCode
        }

        public enum WorkFlowTableName
        {
            WorkFlowProcedure,
            WorkFlowProcedureProperty,
            WorkFlowRole,
            WorkFlowRoleComprise,
            WorkFlowRouter,
            WorkFlowCondition,
            WorkFlowTask,
            WorkFlowTaskActor
        }
    }
}

