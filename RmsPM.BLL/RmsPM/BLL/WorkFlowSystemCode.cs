namespace RmsPM.BLL
{
    using System;
    using Rms.WorkFlow;
    using RmsPM.DAL.EntityDAO;

    public class WorkFlowSystemCode : ISystemCode
    {
        public string GetNewSysetmCode(string systemCodeName)
        {
            return SystemManageDAO.GetNewSysCode(systemCodeName);
        }
    }
}

