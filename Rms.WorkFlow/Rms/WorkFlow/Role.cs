namespace Rms.WorkFlow
{
    using System;
    using System.Collections;

    public class Role
    {
        private string _IsAllUser;
        private string _ProcedureCode;
        private string _Remak;
        private Hashtable _RoleComprises = new Hashtable();
        private string _RoleName;
        private Hashtable _Roles = new Hashtable();
        private string _RoleType;
        private string _WorkFlowRoleCode;

        public void AddNewRole(Role RoleCase)
        {
            this._Roles.Add(RoleCase.WorkFlowRoleCode, RoleCase);
        }

        public void AddNewRoleComprise(RoleComprise RoleCompriseCase)
        {
            this._RoleComprises.Add(RoleCompriseCase.WorkFlowRoleCompriseCode, RoleCompriseCase);
        }

        public void ClearRoleComprises()
        {
            this._RoleComprises.Clear();
        }

        public Role GetRole(string WorkFlowRoleCode)
        {
            return (Role) this._Roles[WorkFlowRoleCode];
        }

        public RoleComprise GetRoleComprise(string WorkFlowRoleCompriseCode)
        {
            return (RoleComprise) this._RoleComprises[WorkFlowRoleCompriseCode];
        }

        public IDictionaryEnumerator GetRoleCompriseEnumerator()
        {
            return this._RoleComprises.GetEnumerator();
        }

        public IDictionaryEnumerator GetRoleCompriseEnumerator(string WorkFlowRoleCode)
        {
            return this._RoleComprises.GetEnumerator();
        }

        public IDictionaryEnumerator GetRoleEnumerator(string ProcedureCode)
        {
            return this._Roles.GetEnumerator();
        }

        public void RemoveRole(string WorkFlowRoleCode)
        {
            this._Roles.Remove(WorkFlowRoleCode);
        }

        public void RemoveRoleComprise(string WorkFlowRoleCompriseCode)
        {
            this._RoleComprises.Remove(WorkFlowRoleCompriseCode);
        }

        public string IsAllUser
        {
            get
            {
                return this._IsAllUser;
            }
            set
            {
                this._IsAllUser = value;
            }
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

        public string Remak
        {
            get
            {
                return this._Remak;
            }
            set
            {
                this._Remak = value;
            }
        }

        public string RoleName
        {
            get
            {
                return this._RoleName;
            }
            set
            {
                this._RoleName = value;
            }
        }

        public string RoleType
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

        public string WorkFlowRoleCode
        {
            get
            {
                return this._WorkFlowRoleCode;
            }
            set
            {
                this._WorkFlowRoleCode = value;
            }
        }
    }
}

