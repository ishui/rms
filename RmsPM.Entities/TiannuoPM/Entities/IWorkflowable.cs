namespace TiannuoPM.Entities
{
    using System;

    public interface IWorkflowable
    {
        void Approve();
        void Reject();
        void Send();
    }
}

