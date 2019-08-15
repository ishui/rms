namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.WorkFlow;
    using RmsPM.DAL.EntityDAO;

    public class WorkFlowDefine : IDefinition
    {
        public DataSet InputDefinition(string procedureCode)
        {
            return WorkFlowDAO.GetStandard_WorkFlowProcedureByCode(procedureCode);
        }

        public void OutputDefinition(DataSet ds, string procedureCode)
        {
            WorkFlowRule.SaveWorkFlowProcedure(ds, procedureCode);
        }
    }
}

