namespace Rms.WorkFlow
{
    using System;
    using System.Data;

    public interface IDefinition
    {
        DataSet InputDefinition(string procedureCode);
        void OutputDefinition(DataSet ds, string procedureCode);
    }
}

