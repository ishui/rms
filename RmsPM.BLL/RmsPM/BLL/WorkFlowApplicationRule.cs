namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;

    public sealed class WorkFlowApplicationRule
    {
        public static void SaveContractAuditing(DataSet workCaseDs, EntityData applicationEntity, EntityData contractEntity, string caseCode)
        {
            try
            {
                EntityData entitydata = WorkFlowRule.SaveWorkFlowCaseData(workCaseDs, caseCode);
                StandardEntityDAO ydao = new StandardEntityDAO("Standard_WorkFlowCase");
                ydao.BeginTrans();
                try
                {
                    try
                    {
                        if (entitydata != null)
                        {
                            ydao.SubmitEntity(entitydata);
                        }
                        ydao.EntityName = "WorkFlow_Leave";
                        ydao.SubmitEntity(applicationEntity);
                        ydao.EntityName = "Standard_Contract";
                        ydao.SubmitEntity(contractEntity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception)
                    {
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
                finally
                {
                    ydao.Dispose();
                }
                entitydata.Dispose();
            }
            catch
            {
                throw;
            }
        }

        public static void SaveWorkCase(DataSet workCaseDs, EntityData applicationEntity, string caseCode, string BillType)
        {
            try
            {
                EntityData entitydata = WorkFlowRule.SaveWorkFlowCaseData(workCaseDs, caseCode);
                StandardEntityDAO ydao = new StandardEntityDAO("Standard_WorkFlowCase");
                ydao.BeginTrans();
                try
                {
                    try
                    {
                        if (entitydata != null)
                        {
                            ydao.SubmitEntity(entitydata);
                        }
                        ydao.EntityName = BillType;
                        ydao.SubmitEntity(applicationEntity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception)
                    {
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
                finally
                {
                    ydao.Dispose();
                }
                entitydata.Dispose();
            }
            catch
            {
                throw;
            }
        }

        public static void SaveWorkCase_Leave(DataSet workCaseDs, EntityData applicationEntity, string caseCode)
        {
            try
            {
                EntityData entitydata = WorkFlowRule.SaveWorkFlowCaseData(workCaseDs, caseCode);
                StandardEntityDAO ydao = new StandardEntityDAO("Standard_WorkFlowCase");
                ydao.BeginTrans();
                try
                {
                    try
                    {
                        if (entitydata != null)
                        {
                            ydao.SubmitEntity(entitydata);
                        }
                        ydao.EntityName = "WorkFlow_Leave";
                        ydao.SubmitEntity(applicationEntity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception)
                    {
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
                finally
                {
                    ydao.Dispose();
                }
                entitydata.Dispose();
            }
            catch
            {
                throw;
            }
        }

        public static void SaveWorkCaseEx(DataSet workCaseDs, string caseCode)
        {
            try
            {
                EntityData entitydata = WorkFlowRule.SaveWorkFlowCaseData(workCaseDs, caseCode);
                StandardEntityDAO ydao = new StandardEntityDAO("Standard_WorkFlowCase");
                try
                {
                    if (entitydata != null)
                    {
                        ydao.SubmitEntity(entitydata);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                entitydata.Dispose();
            }
            catch
            {
                throw;
            }
        }

        public static void SaveWorkCaseEx(DataSet workCaseDs, string caseCode, string ActCode)
        {
            try
            {
                EntityData entitydata = WorkFlowRule.SaveWorkFlowCaseData(workCaseDs, caseCode);
                StandardEntityDAO ydao = new StandardEntityDAO("Standard_WorkFlowCase");
                ydao.BeginTrans();
                try
                {
                    if (entitydata != null)
                    {
                        for (int i = entitydata.Tables["WorkFlowAct"].Rows.Count - 1; i >= 0; i--)
                        {
                            if (ActCode == entitydata.Tables["WorkFlowAct"].Rows[i]["ActCode"].ToString())
                            {
                                entitydata.Tables["WorkFlowAct"].Rows[i].Delete();
                            }
                        }
                        ydao.SubmitEntity(entitydata);
                    }
                    ydao.CommitTrans();
                }
                catch (Exception exception)
                {
                    ydao.RollBackTrans();
                    throw exception;
                }
                entitydata.Dispose();
            }
            catch
            {
                throw;
            }
        }
    }
}

