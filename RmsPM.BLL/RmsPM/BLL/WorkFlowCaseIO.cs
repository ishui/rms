namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.WorkFlow;
    using RmsPM.DAL.EntityDAO;

    public class WorkFlowCaseIO : IWorkCase
    {
        public DataSet InputWorkCase(string CaseCode)
        {
            return WorkFlowDAO.GetStandard_WorkFlowCaseByCode(CaseCode);
        }

        public void OutputWorkCase(DataSet ds, string CaseCode)
        {
            WorkFlowRule.SaveWorkFlowCase(ds, CaseCode);
        }
    }
}

