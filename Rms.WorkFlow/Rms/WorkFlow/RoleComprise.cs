namespace Rms.WorkFlow
{
    using System;
    using System.Collections;

    public class RoleComprise
    {
        private string _ProcedureCode;
        private string _RoleCode;
        private string _RoleComprise;
        private Hashtable _RoleComprises = new Hashtable();
        private Rms.WorkFlow.RoleType _RoleType;
        private string _WorkFlowRoleCompriseCode;

        public void AddNewRoleComprise(RoleComprise RoleCompriseCase)
        {
            this._RoleComprises.Add(RoleCompriseCase.WorkFlowRoleCompriseCode, RoleCompriseCase);
        }

        public RoleComprise GetRoleComprise(string WorkFlowRoleCompriseCode)
        {
            return (RoleComprise) this._RoleComprises[WorkFlowRoleCompriseCode];
        }

        public IDictionaryEnumerator GetRoleCompriseEnumerator(string WorkFlowRoleCode)
        {
            return this._RoleComprises.GetEnumerator();
        }

        public void RemoveRoleComprise(string WorkFlowRoleCompriseCode)
        {
            this._RoleComprises.Remove(WorkFlowRoleCompriseCode);
        }

        public string ProcedureCode
        {
            get
            {
                return this._ProcedureCode;
            }
            set
            {
                this._ProcedureCode = value;
            }
        }

        public string RoleCode
        {
            get
            {
                return this._RoleCode;
            }
            set
            {
                this._RoleCode = value;
            }
        }

        public string RoleCompriseItem
        {
            get
            {
                return this._RoleComprise;
            }
            set
            {
                this._RoleComprise = value;
            }
        }

        public Rms.WorkFlow.RoleType RoleType
        {
            get
            {
                return this._RoleType;
            }
            set
            {
                this._RoleType = value;
            }
        }

        public string WorkFlowRoleCompriseCode
        {
            get
            {
                return this._WorkFlowRoleCompriseCode;
            }
            set
            {
                this._WorkFlowRoleCompriseCode = value;
            }
        }
    }
}

