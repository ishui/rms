namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.BLL.RefSal;
    using RmsPM.DAL.EntityDAO;

    public class DtsPayRule
    {
        public static void ClearDtsPay(string ProjectCode)
        {
            QueryAgent agent = new QueryAgent();
            try
            {
                try
                {
                    agent.ExecuteSql("exec sp_dts_clear_pay '" + ProjectCode + "'");
                    string projectCode = ProjectRule.GetSalProjectCode(ProjectCode);
                    if ((projectCode != "") || (ProjectCode == ""))
                    {
                        new SalService().ClearSalImpFlag(projectCode);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            finally
            {
                agent.Dispose();
            }
        }

        public static void DtsPaySingleByClient(string ClientCode)
        {
            Exception exception;
            try
            {
                SalService srv = new SalService();
                DataSet dsSrc = srv.GetSalDataByClient(ClientCode);
                DataTable table = dsSrc.Tables["Client"];
                if (table.Rows.Count > 0)
                {
                    string projectCode = ProjectRule.GetProjectCodeBySalProjectCode(table.Rows[0]["proj_code"].ToString());
                    switch (projectCode)
                    {
                        case null:
                        case "":
                            throw new Exception("该客户在项目管理系统中无对应的项目");
                    }
                    using (StandardEntityDAO dao = new StandardEntityDAO("SalClient"))
                    {
                        dao.BeginTrans();
                        try
                        {
                            EntityData data = ImportSalClientByClient(ClientCode, projectCode, dsSrc, dao);
                            EntityData data2 = ImportSalContractByClient(ClientCode, projectCode, dsSrc, dao);
                            EntityData data3 = ImportSalPayByClient(ClientCode, projectCode, dsSrc, dao);
                            EntityData data4 = ImportSalPayPlanByClient(ClientCode, projectCode, dsSrc, dao);
                            EntityData data5 = ImportSalPayRelaByClient(ClientCode, projectCode, dsSrc, dao);
                            dao.CommitTrans();
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            try
                            {
                                dao.RollBackTrans();
                            }
                            catch
                            {
                            }
                            throw exception;
                        }
                        SetSalImpFlag(srv, dsSrc);
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static DataTable GetDtsPayByClient(string ProjectCode, string ClientName)
        {
            string projectCode = "";
            if ((ProjectCode == null) || (ProjectCode == ""))
            {
                projectCode = ProjectRule.GetAllSalProjectCode();
            }
            else
            {
                projectCode = ProjectRule.GetSalProjectCode(ProjectCode);
            }
            SalService service = new SalService();
            return service.GetSalDataHeadClientByClientName(projectCode, ClientName).Tables[0];
        }

        public static DataTable GetDtsPayByProject(string ProjectCode)
        {
            string projectCode = "";
            if ((ProjectCode == null) || (ProjectCode == ""))
            {
                projectCode = ProjectRule.GetAllSalProjectCode();
            }
            else
            {
                projectCode = ProjectRule.GetSalProjectCode(ProjectCode);
            }
            SalService service = new SalService();
            return service.GetSalDataHeadClientByProject(projectCode).Tables[0];
        }

        public static DataTable GetSalProject()
        {
            DataTable table2;
            try
            {
                SalService service = new SalService();
                DataTable table = service.GetSalProject().Tables[0];
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetSalProjectByCode(string ProjectCode)
        {
            DataTable table2;
            try
            {
                SalService service = new SalService();
                DataTable table = service.GetSalProjectByCode(ProjectCode).Tables[0];
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetSalProjectNameByCode(string ProjectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (ProjectCode == "")
                {
                    return text;
                }
                SalService service = new SalService();
                DataTable table = service.GetSalProjectByCode(ProjectCode).Tables[0];
                if (table.Rows.Count > 0)
                {
                    text = ConvertRule.ToString(table.Rows[0]["Proj_Name"]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSuplCodeByName(string SuplName, string ProjectCode)
        {
            string text = "";
            object obj2 = null;
            if (SuplName != "")
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    obj2 = agent.ExecuteScalar("select top 1 SuplCode from SalSupl where SuplName = '" + SuplName + "' and ProjectCode = '" + ProjectCode + "'");
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        return obj2.ToString();
                    }
                    obj2 = agent.ExecuteScalar("select top 1 SuplCode from SalSupl where SuplName = '" + SuplName + "' and isnull(ProjectCode, '') = ''");
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        text = obj2.ToString();
                    }
                }
                finally
                {
                    agent.Dispose();
                }
            }
            return text;
        }

        public static string GetSuplNameByContract(string ContractID, int type)
        {
            string text = "";
            try
            {
                string s;
                switch (type)
                {
                    case 1:
                        return ContractID.Substring(0, 13);

                    case 2:
                        s = "";
                        text = ContractID.Substring(11, 2);
                        s = ContractID.Substring(0, 2);
                        if (int.Parse(s) <= 70)
                        {
                            break;
                        }
                        text = text + s;
                        goto Label_0085;

                    case 3:
                        return ("20" + ContractID.Substring(2, ContractID.Length - 2).Replace("-", "－"));

                    case 4:
                        return ("20" + ContractID.Substring(0, 2) + ContractID.Substring(10, 2) + int.Parse(ContractID.Substring(7, 3)).ToString());

                    case 5:
                        return ("20" + ContractID.Substring(0, 2) + ContractID.Substring(11, 2) + int.Parse(ContractID.Substring(7, 4)).ToString());

                    case 6:
                        return ("20" + ContractID.Substring(0, 2) + ContractID.Substring(12, 2) + int.Parse(ContractID.Substring(7, 5)).ToString());

                    default:
                        return text;
                }
                text = text + "20" + s;
            Label_0085:
                text = text + "-" + int.Parse(ContractID.Substring(7, 4)).ToString();
            }
            catch
            {
            }
            return text;
        }

        private static EntityData ImportSalClientByClient(string ClientCode, string ProjectCode, DataSet dsSrc, StandardEntityDAO dao)
        {
            EntityData data2;
            try
            {
                DataRow row;
                DataTable table = dsSrc.Tables["Client"];
                DataRow row2 = table.Rows[0];
                dao.EntityName = "SalClient";
                EntityData entitydata = new EntityData("SalClient");
                entitydata = dao.SelectbyPrimaryKey(ClientCode);
                if (entitydata.HasRecord())
                {
                    row = entitydata.CurrentTable.Rows[0];
                }
                else
                {
                    row = entitydata.CurrentTable.NewRow();
                }
                row["ProjectCode"] = ProjectCode;
                row["ClientCode"] = row2["client_code"];
                row["ClientName"] = row2["client_name"];
                if (!entitydata.HasRecord())
                {
                    entitydata.AddNewRecord(row);
                    dao.InsertEntity(entitydata);
                }
                else
                {
                    dao.UpdateEntity(entitydata);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private static EntityData ImportSalContractByClient(string ClientCode, string ProjectCode, DataSet dsSrc, StandardEntityDAO dao)
        {
            EntityData data2;
            try
            {
                DataTable table = dsSrc.Tables["Contract"];
                dao.EntityName = "SalContract";
                EntityData entitydata = new EntityData("SalContract");
                string[] Params = new string[] { "@ClientCode" };
                object[] values = new object[] { ClientCode };
                dao.FillEntity(SqlManager.GetSqlStruct("SalContract", "SelectByClient").SqlString, Params, values, entitydata, "SalContract");
                if (entitydata.HasRecord())
                {
                    dao.DeleteAllRow(entitydata);
                    dao.DeleteEntity(entitydata);
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = entitydata.CurrentTable.NewRow();
                    DataRow row2 = table.Rows[i];
                    string contractCode = row2["contract_code"].ToString();
                    row["ProjectCode"] = ProjectCode;
                    row["ContractCode"] = contractCode;
                    row["ContractID"] = row2["contract_id"];
                    row["ClientCode"] = row2["client_code"];
                    row["ClientName"] = row2["client_name"];
                    row["RoomCode"] = row2["room_code"];
                    row["ContractDate"] = row2["contract_date"];
                    row["TotalPrice"] = row2["total_price"];
                    row["FactPrice"] = row2["fact_price"];
                    row["UnitPrice"] = row2["unit_price"];
                    //row["Jiesuan"] = row2["jiesuan"];
                   // row["BofangCode"] = row2["bofang_code"];
                    row["ChamberName"] = row2["chamber"];
                    row["BuildDim"] = row2["build_dim"];
                    row["RoomDim"] = row2["room_dim"];
                    row["Room"] = row2["room"];
                    row["BuildingName"] = row2["build_name"];
                    //row["JiesuanDate"] = row2["jiesuan_date"];
                    string suplCodeByName = GetSuplCodeByName(GetSuplNameByContract(row["ContractID"].ToString(), 4), ProjectCode);
                    row["SuplCode"] = suplCodeByName;
                    string roomCode = ProductRule.GetRoomCodeByChamberRoomName(ConvertRule.ToString(row["ChamberName"]), ConvertRule.ToString(row["Room"]), ProjectCode);
                    row["RoomCode"] = roomCode;
                    UpdateRoomSalState(roomCode, contractCode);
                    entitydata.AddNewRecord(row);
                    dao.InsertEntity(entitydata);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private static EntityData ImportSalPayByClient(string ClientCode, string ProjectCode, DataSet dsSrc, StandardEntityDAO dao)
        {
            EntityData data2;
            try
            {
                DataTable table = dsSrc.Tables["account"];
                DataTable table2 = dsSrc.Tables["SalPayPlan"];
                DataTable table3 = dsSrc.Tables["SalPayRela"];
                dao.EntityName = "SalPay";
                EntityData entitydata = new EntityData("SalPay");
                string[] Params = new string[] { "@ClientCode" };
                object[] values = new object[] { ClientCode };
                dao.FillEntity(SqlManager.GetSqlStruct("SalPay", "SelectByClient").SqlString, Params, values, entitydata, "SalPay");
                if (entitydata.HasRecord())
                {
                    dao.DeleteAllRow(entitydata);
                    dao.DeleteEntity(entitydata);
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = entitydata.CurrentTable.NewRow();
                    DataRow row2 = table.Rows[i];
                    row["ClientCode"] = ClientCode;
                    row["ProjectCode"] = ProjectCode;
                    row["PayCode"] = row2["account_code"];
                    row["AccountCode"] = row2["account_code"];
                    row["PayDate"] = row2["account_date"];
                    row["PayMoney"] = row2["account_price"];
                    row["PayType"] = row2["account_type"];
                    row["Remark"] = row2["remark"];
                    row["CheckDate"] = row2["check_date"];
                    row["CheckMan"] = row2["check_man"];
                    string text = "";
                    DataRow[] rowArray = table3.Select("account_code = '" + row2["account_code"].ToString() + "'");
                    if (rowArray.Length > 0)
                    {
                        string text2 = rowArray[0]["plan_code"].ToString();
                        if (text2 != "")
                        {
                            DataRow[] rowArray2 = table2.Select("pay_plan_code = '" + text2 + "'");
                            if (rowArray2.Length > 0)
                            {
                                text = rowArray2[0]["contract_code"].ToString();
                            }
                        }
                    }
                    row["ContractCode"] = text;
                    entitydata.AddNewRecord(row);
                    dao.InsertEntity(entitydata);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private static EntityData ImportSalPayPlanByClient(string ClientCode, string ProjectCode, DataSet dsSrc, StandardEntityDAO dao)
        {
            EntityData data2;
            try
            {
                DataTable table = dsSrc.Tables["SalPayPlan"];
                dao.EntityName = "SalPayPlan";
                EntityData entitydata = new EntityData("SalPayPlan");
                string[] Params = new string[] { "@ClientCode" };
                object[] values = new object[] { ClientCode };
                dao.FillEntity(SqlManager.GetSqlStruct("SalPayPlan", "SelectByClient").SqlString, Params, values, entitydata, "SalPayPlan");
                if (entitydata.HasRecord())
                {
                    dao.DeleteAllRow(entitydata);
                    dao.DeleteEntity(entitydata);
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = entitydata.CurrentTable.NewRow();
                    DataRow row2 = table.Rows[i];
                    row["PayPlanCode"] = row2["pay_plan_code"];
                    row["ClientCode"] = ClientCode;
                    row["ProjectCode"] = ProjectCode;
                    row["ContractCode"] = row2["contract_code"];
                    row["PayMode"] = row2["pay_mode"];
                    row["Prompt"] = row2["Prompt"];
                    row["PlanMoney"] = row2["price"];
                    row["ItemName"] = row2["item_name"];
                    entitydata.AddNewRecord(row);
                    dao.InsertEntity(entitydata);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        private static EntityData ImportSalPayRelaByClient(string ContractCode, string ProjectCode, DataSet dsSrc, StandardEntityDAO dao)
        {
            EntityData data2;
            try
            {
                DataTable table = dsSrc.Tables["SalPayRela"];
                dao.EntityName = "SalPayRela";
                EntityData entitydata = new EntityData("SalPayRela");
                string[] Params = new string[] { "@ContractCode" };
                object[] values = new object[] { ContractCode };
                dao.FillEntity(SqlManager.GetSqlStruct("SalPayRela", "SelectByContract").SqlString, Params, values, entitydata, "SalPayRela");
                if (entitydata.HasRecord())
                {
                    dao.DeleteAllRow(entitydata);
                    dao.DeleteEntity(entitydata);
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = entitydata.CurrentTable.NewRow();
                    DataRow row2 = table.Rows[i];
                    row["SystemID"] = row2["account_plan_code"].ToString();
                    row["ProjectCode"] = ProjectCode;
                    row["ContractCode"] = ContractCode;
                    row["PayPlanCode"] = row2["plan_code"].ToString();
                    row["PayCode"] = row2["account_code"].ToString();
                    row["PayMoney"] = row2["price"].ToString();
                    entitydata.AddNewRecord(row);
                    dao.InsertEntity(entitydata);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void MapSalRoom(string ProjectCode)
        {
            try
            {
                EntityData entity = SalDAO.GetSalContractByProjectCode(ProjectCode);
                if (entity.HasRecord())
                {
                    int count = entity.CurrentTable.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        entity.SetCurrentRow(i);
                        string contractCode = entity.GetString("ContractCode");
                        string chamberName = entity.GetString("ChamberName");
                        string roomName = entity.GetString("Room");
                        string roomCode = ProductRule.GetRoomCodeByChamberRoomName(chamberName, roomName, ProjectCode);
                        if (roomCode != entity.GetString("RoomCode"))
                        {
                            entity.CurrentTable.Rows[i]["RoomCode"] = roomCode;
                            SalDAO.UpdateSalContract(entity);
                        }
                        UpdateRoomSalState(roomCode, contractCode);
                    }
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void MapSalSupl(string ProjectCode, int type)
        {
            try
            {
                EntityData entity = SalDAO.GetSalContractByProjectCode(ProjectCode);
                if (entity.HasRecord())
                {
                    int count = entity.CurrentTable.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        entity.SetCurrentRow(i);
                        string suplCodeByName = GetSuplCodeByName(GetSuplNameByContract(entity.GetString("ContractID"), type), ProjectCode);
                        if ((suplCodeByName != "") && (suplCodeByName != entity.GetString("SuplCode")))
                        {
                            entity.CurrentTable.Rows[i]["SuplCode"] = suplCodeByName;
                            SalDAO.UpdateSalContract(entity);
                        }
                    }
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void SetSalImpFlag(SalService srv, DataSet dsSrc)
        {
            DataTable table = dsSrc.Tables["account"];
            if (table.Rows.Count > 0)
            {
                string accountCodes = "";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (accountCodes != "")
                    {
                        accountCodes = accountCodes + ",";
                    }
                    accountCodes = accountCodes + table.Rows[i]["account_code"].ToString();
                }
                srv.SetSalAccountImpFlag(accountCodes);
            }
            if (dsSrc.Tables.Contains("contract"))
            {
                DataTable table2 = dsSrc.Tables["contract"];
                if ((table2.Rows.Count > 0) && (table2.Rows[0]["jiesuan"].ToString() == "1"))
                {
                    string contractCodes = table2.Rows[0]["contract_code"].ToString();
                    srv.SetSalContractImpFlag(contractCodes);
                }
            }
        }

        public static void UpdateRoomSalState(string RoomCode, string ContractCode)
        {
            try
            {
                if (RoomCode != "")
                {
                    EntityData entity = ProductDAO.GetRoomByCode(RoomCode);
                    if (entity.HasRecord() && (ConvertRule.ToString(entity.CurrentRow["ContractCode"]) != ContractCode))
                    {
                        entity.CurrentRow["SalState"] = "已售";
                        entity.CurrentRow["ContractCode"] = ContractCode;
                        ProductDAO.UpdateRoom(entity);
                    }
                    entity.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

