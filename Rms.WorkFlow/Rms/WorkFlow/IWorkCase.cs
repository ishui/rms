namespace Rms.WorkFlow
{
    using System;
    using System.Data;

    public interface IWorkCase
    {
        DataSet InputWorkCase(string caseCode);
        void OutputWorkCase(DataSet ds, string workCaseCode);
    }
}

